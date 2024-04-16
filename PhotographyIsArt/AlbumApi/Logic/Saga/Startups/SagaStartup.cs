using AlbumApi.Logic.Saga.Consumers;
using AlbumApi.Logic.Saga.Db;
using AlbumApi.Logic.Saga.States;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace AlbumApi.Logic.Saga.Startups
{
    public static class SagaStartup
    {
        public static IServiceCollection TryAddSaga(this IServiceCollection services, IConfiguration configuration)
        {
			services.AddDbContext<AlbumIdentityDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgresDb")));
			services.AddMassTransit(config =>
			{
				config.SetKebabCaseEndpointNameFormatter();
				config.SetInMemorySagaRepositoryProvider();
				config.AddDelayedMessageScheduler();
				config.AddConsumer<AlbumIdentitySagaConsumer>();
				config.AddSagaStateMachine<AlbumIdentitySagaStateMachine, AlbumIdentitySagaState>()
					.EntityFrameworkRepository(repo =>
					{
						repo.ConcurrencyMode = ConcurrencyMode.Pessimistic;
						repo.ExistingDbContext<AlbumIdentityDbContext>();
						repo.LockStatementProvider = new SqlServerLockStatementProvider();
					});
				config.UsingRabbitMq((bus, rabbitBus) =>
				{
					rabbitBus.UseInMemoryOutbox(bus);
					rabbitBus.UseMessageRetry(retry =>
					{
						retry.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
					});
					rabbitBus.UseDelayedMessageScheduler();
					rabbitBus.Host("localhost", data =>
					{
						data.Username("guest");
						data.Password("guest");
					});
					rabbitBus.ConfigureEndpoints(bus);
				});
			});

			return services;
		}
    }
}
