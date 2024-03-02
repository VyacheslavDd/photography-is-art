using AlbumApi.Dal.Albums.Models;
using AlbumApi.Dal.Tags.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbumApi.Dal
{
	public class AlbumDbContext : DbContext
	{
		public DbSet<AlbumDal> Albums { get; set; }
		public DbSet<AlbumTagDal> Tags { get; set; }

		public AlbumDbContext(DbContextOptions<AlbumDbContext> options) : base(options)
		{
			Database.Migrate();
		}
	}
}
