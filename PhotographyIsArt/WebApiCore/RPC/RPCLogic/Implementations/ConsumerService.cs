using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiCore.RPC.RPCLogic.Interfaces;
using WebApiCore.RPC.RPCModels.Data;

namespace WebApiCore.RPC.RPCLogic.Implementations
{
	internal class ConsumerService : BackgroundService, IConsumerService, IDisposable
	{
		private readonly IConnectionFactory _connectionFactory;
		private readonly IOptions<RPCConfigurationData> _rpcConfig;
		private readonly IConnection _connection;
		private readonly IModel _channel;

		public ConsumerService(IConnectionFactory connectionFactory, IOptions<RPCConfigurationData> rpcConfig)
		{
			_connectionFactory = connectionFactory;
			_rpcConfig = rpcConfig;
			_connection = _connectionFactory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		public void OnReceived(object model, BasicDeliverEventArgs eventArgs, IModel channel)
		{
			string response = string.Empty;

			var body = eventArgs.Body.ToArray();
			var props = eventArgs.BasicProperties;
			var replyProps = channel.CreateBasicProperties();
			replyProps.CorrelationId = props.CorrelationId;

			try
			{
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"Got message ({message})");
				response = $"{message} back to you!";
			}
			catch (Exception e)
			{
				Console.WriteLine($" [.] {e.Message}");
				response = string.Empty;
			}
			finally
			{
				var responseBytes = Encoding.UTF8.GetBytes(response);
				channel.BasicPublish(_rpcConfig.Value.ExchangeName, props.ReplyTo, replyProps, responseBytes);
				channel.BasicAck(eventArgs.DeliveryTag, false);
			}
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_channel.ExchangeDeclare(_rpcConfig.Value.ExchangeName, _rpcConfig.Value.ExchangeType);
			_channel.QueueDeclare(_rpcConfig.Value.ConsumerQueue, _rpcConfig.Value.IsDurable,
				_rpcConfig.Value.IsExclusive, _rpcConfig.Value.AutoDelete);
			_channel.QueueBind(_rpcConfig.Value.ConsumerQueue, _rpcConfig.Value.ExchangeName, _rpcConfig.Value.RoutingKey);
			_channel.BasicQos(_rpcConfig.Value.PrefetchSize, _rpcConfig.Value.PrefetchCount, _rpcConfig.Value.IsGlobal);
			var consumer = new EventingBasicConsumer(_channel);
			_channel.BasicConsume(_rpcConfig.Value.ConsumerQueue, false, consumer);
			consumer.Received += (model, ea) => OnReceived(model, ea, _channel);
			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_channel.Close();
			_connection.Close();
		}
	}
}
