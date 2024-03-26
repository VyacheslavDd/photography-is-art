using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Exceptions;
using WebApiCore.Exceptions.Enums;
using WebApiCore.Pool.Logic.Interfaces;

namespace WebApiCore.Pool.Logic
{
	public class RPCConnectionPool : IPool<IConnection>
	{
		private readonly ConcurrentBag<IConnection> _connections;
		private readonly IConnectionFactory _connectionFactory;

		public RPCConnectionPool(IConnectionFactory connectionFactory, int initialSize=5)
		{
			_connections = new ConcurrentBag<IConnection>();
			_connectionFactory = connectionFactory;

			for (int i = 0; i < initialSize; i++)
			{
				var connection = _connectionFactory.CreateConnection();
				_connections.Add(connection);
			}
		}

		public IConnection Get()
		{
			if (_connections.TryTake(out var connection))
			{
				return connection;
			}
			ExceptionHandler.ThrowException(ExceptionType.NoConnectionsAvailable, "Нет доступных соединений! Попробуйте позже.");
			return null;
		}

		public void Return(IConnection connection)
		{
			_connections.Add(connection);
		}
	}
}
