namespace AlbumApi.Dal.Filters
{
	public class AlbumQueryFilter
	{
		public Guid UserId { get; set; }
		public bool CollectUserData { get; set; } = true;
	}
}
