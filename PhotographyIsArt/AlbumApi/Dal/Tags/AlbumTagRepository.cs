using AlbumApi.Dal.Albums.Models;
using AlbumApi.Dal.Tags.Interfaces;
using AlbumApi.Dal.Tags.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using WebApiCore.Dal.Base.Interfaces;
using WebApiCore.Dal.Base.Repositories;

namespace AlbumApi.Dal.Tags
{
	public class AlbumTagRepository : BaseRepository<AlbumTagDal>
	{
		private readonly AlbumDbContext _context;

		public AlbumTagRepository(AlbumDbContext context) : base(context.Tags, context)
		{
			_context = context;
		}

		public override async Task<AlbumTagDal?> GetByGuidAsync(Guid guid)
		{
			return await _context.Tags.Include(t => t.Albums).FirstOrDefaultAsync(t => t.Id == guid);
		}
	}
}
