﻿using AutoMapper;
using BCrypt.Net;
using IdentityApi.Api.Controllers.PostModels.Roles;
using IdentityApi.Api.Controllers.PostModels.Users;
using IdentityApi.Api.Controllers.ViewModels.Roles;
using IdentityApi.Api.Controllers.ViewModels.Users;
using IdentityApi.Domain.Entities;

namespace IdentityApi.Infrastructure.Mappings
{
	public class IdentityServerMapper : Profile
	{
		public IdentityServerMapper()
		{
			CreateMap<RegistrationRequest, User>()
				.ForMember(u => u.ProfilePictureUrl, opt => opt.MapFrom(r => string.Empty))
				.ForMember(u => u.UserInfo, opt => opt.MapFrom(r => string.Empty))
				.ForMember(u => u.BirthDate, opt => opt.MapFrom(r => r.BirthDate.ToUniversalTime()))
				.ForMember(u => u.Password, opt => opt.MapFrom(r => BCrypt.Net.BCrypt.HashPassword(r.Password)))
				.ForMember(u => u.Roles, opt => opt.MapFrom(r => new List<Role>()));
			CreateMap<LoginRequest, User>();
			CreateMap<UpdateUserRequest, User>()
				.ForMember(u => u.BirthDate, opt => opt.MapFrom(r => r.BirthDate.ToUniversalTime()))
				.ForMember(u => u.Password, opt => opt.MapFrom(r => BCrypt.Net.BCrypt.HashPassword(r.Password)));
			CreateMap<User, GetShortUserResponse>();
			CreateMap<User, GetFullUserResponse>();

			CreateMap<CreateRoleRequest, Role>()
				.ForMember(r => r.Users, opt => opt.MapFrom(req => new List<User>()));
			CreateMap<UpdateRoleRequest, Role>();
			CreateMap<Role, GetRoleResponse>();
		}
	}
}
