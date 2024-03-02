using IdentityApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Infrastructure.Contexts
{
	public class UsersDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
		{
			Database.Migrate();
		}
	}
}
