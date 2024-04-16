using MassTransit.Saga;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AlbumApi.Logic.Saga.Db
{
	public class SagaContextFactory<TSaga> : ISagaRepositoryContextFactory<TSaga> where TSaga : class, ISaga
	{
		private readonly DbContextOptions _options;

		public SagaContextFactory(DbContextOptions options)
		{
			_options = options;
		}

		public void Probe(ProbeContext context)
		{
			throw new NotImplementedException();
		}

		public Task Send<T>(ConsumeContext<T> context, IPipe<SagaRepositoryContext<TSaga, T>> next) where T : class
		{
			throw new NotImplementedException();
		}

		public Task SendQuery<T>(ConsumeContext<T> context, ISagaQuery<TSaga> query, IPipe<SagaRepositoryQueryContext<TSaga, T>> next) where T : class
		{
			throw new NotImplementedException();
		}
	}
}
