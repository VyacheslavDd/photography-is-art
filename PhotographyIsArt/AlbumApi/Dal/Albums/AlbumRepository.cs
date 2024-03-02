using AlbumApi.Dal.Albums.Interfaces;
using AlbumApi.Dal.Albums.Models;
using AlbumApi.Dal.Tags.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using WebApiCore.Dal.Base.Interfaces;
using WebApiCore.Dal.Base.Repositories;

namespace AlbumApi.Dal.Albums
{
	public class AlbumRepository : BaseRepository<AlbumDal>
	{
		private readonly AlbumDbContext _context;

		public AlbumRepository(AlbumDbContext context) : base(context.Albums, context)
		{
			_context = context;
		}

		public override async Task<AlbumDal?> GetByGuidAsync(Guid guid)
		{
			return await _context.Albums.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == guid);
		}
	}
}
