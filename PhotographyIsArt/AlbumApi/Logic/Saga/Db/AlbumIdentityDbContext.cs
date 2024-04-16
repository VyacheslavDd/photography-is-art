using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace AlbumApi.Logic.Saga.Db
{
	public class AlbumIdentityDbContext : SagaDbContext
	{
		protected override IEnumerable<ISagaClassMap> Configurations { get; }

		public AlbumIdentityDbContext(DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
