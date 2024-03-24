using Microsoft.Extensions.Hosting;
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

namespace WebApiCore.RPC.RPCLogic.Implementations
{
	internal class ConsumerService : BackgroundService, IConsumerService
	{
		private readonly IConnectionFactory _connectionFactory;

		public ConsumerService(IConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
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
				channel.BasicPublish("photo.test", props.ReplyTo, replyProps, responseBytes);
				channel.BasicAck(eventArgs.DeliveryTag, false);
			}
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var connection = _connectionFactory.CreateConnection();
			var channel = connection.CreateModel();
			channel.ExchangeDeclare("photo.test", "direct");
			channel.QueueDeclare("testing", false, false, false);
			channel.QueueBind("testing", "photo.test", "key");
			channel.BasicQos(0, 1, false);
			var consumer = new EventingBasicConsumer(channel);
			channel.BasicConsume("testing", false, consumer);
			consumer.Received += (model, ea) => OnReceived(model, ea, channel);
			return Task.CompletedTask;
		}
	}
}
