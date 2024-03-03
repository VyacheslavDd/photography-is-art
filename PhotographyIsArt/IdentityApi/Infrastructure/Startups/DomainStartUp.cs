using IdentityApi.Domain.Interfaces;
using IdentityApi.Infrastructure.Contexts;
using IdentityApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityApi.Infrastructure.Startups
{
    public static class DomainStartUp
    {
        public static IServiceCollection TryAddDomain(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UsersDbContext>(opt => opt.UseNpgsql(config.GetConnectionString("PostgresDb")));
            services.TryAddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
