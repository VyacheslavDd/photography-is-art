using WebApiCore.Dal.Base.Models;

namespace IdentityApi.Domain.Entities
{
	public record RefreshToken : BaseModel<Guid>
	{
		public required string Token {  get; set; }
		public required DateTime Created {  get; set; }
		public required DateTime Expires { get; set; }
		public required Guid UserId { get; set; }
		public required User User { get; set; }
	}
}
