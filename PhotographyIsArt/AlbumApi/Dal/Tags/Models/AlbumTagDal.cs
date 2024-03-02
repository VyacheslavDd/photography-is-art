using AlbumApi.Dal.Albums.Models;
using WebApiCore.Dal.Base.Models;

namespace AlbumApi.Dal.Tags.Models
{
    //что-то типа тега для альбома (природа, животные...)
    public record AlbumTagDal : BaseModel<Guid>
    {
        public string? Name { get; set; }
        public List<AlbumDal?> Albums { get; set; }
    }
}
