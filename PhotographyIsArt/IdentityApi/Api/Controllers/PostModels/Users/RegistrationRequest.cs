using IdentityApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace IdentityApi.Api.Controllers.PostModels.Users
{
	public class RegistrationRequest
	{
		public string? Name { get; set; }
		public string? Login { get; set; }
		public Gender Gender { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
