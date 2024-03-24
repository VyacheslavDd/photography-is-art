﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.RPC.RPCLogic.Interfaces;

namespace WebApiCore.RPC.RPCLogic.Implementations
{
	internal class ProducerClient : IProducerClient
	{
		private readonly string _routingKey;
		private readonly IConnection _connection;
		private readonly IModel _channel;
		private readonly string _replyQueueName;
		private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _callbackMapper = new();

		public ProducerClient(IConnectionFactory connectionFactory)
		{
			_routingKey = "key";
			_connection = connectionFactory.CreateConnection();
			_channel = _connection.CreateModel();
			_replyQueueName = _channel.QueueDeclare().QueueName;
			_channel.QueueBind(_replyQueueName, "photo.test", _replyQueueName);
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, ea) => OnReceived(model, ea);
			_channel.BasicConsume(_replyQueueName, true, consumer);
		}

		public Task<string> CallAsync(string message, CancellationToken cancellationToken = default)
		{
			var props = _channel.CreateBasicProperties();
			var correlationId = Guid.NewGuid().ToString();
			props.CorrelationId = correlationId;
			props.ReplyTo = _replyQueueName;
			var messageBytes = Encoding.UTF8.GetBytes(message);
			var tcs = new TaskCompletionSource<string>();
			_callbackMapper.TryAdd(correlationId, tcs);

			_channel.BasicPublish("photo.test", _routingKey, props, messageBytes);

			cancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));
			return tcs.Task;
		}

		public void OnReceived(object model, BasicDeliverEventArgs eventArgs)
		{
			if (!_callbackMapper.TryRemove(eventArgs.BasicProperties.CorrelationId, out var tsk))
				return;
			var body = eventArgs.Body.ToArray();
			var response = Encoding.UTF8.GetString(body);
			Console.WriteLine(response);
			tsk.SetResult(response);
		}
	}
}