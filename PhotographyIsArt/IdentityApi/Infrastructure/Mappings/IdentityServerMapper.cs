using AutoMapper;
using IdentityApi.Api.Controllers.PostModels.Users;
using IdentityApi.Api.Controllers.ViewModels.Users;
using IdentityApi.Domain.Entities;

namespace IdentityApi.Infrastructure.Mappings
{
	public class IdentityServerMapper : Profile
	{
		public IdentityServerMapper()
		{
			CreateMap<RegistrationRequest, User>();
			CreateMap<LoginRequest, User>();
			CreateMap<UpdateUserRequest, User>();
			CreateMap<User, GetUsersResponse>();
			CreateMap<User, GetUserResponse>();
		}
	}
}
