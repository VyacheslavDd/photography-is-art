﻿using IdentityApi.Infrastructure.Filters;
using IdentityApi.Services.Implementations.Roles;
using IdentityApi.Services.Implementations.Tokens;
using IdentityApi.Services.Implementations.Users;
using IdentityApi.Services.Interfaces.Roles;
using IdentityApi.Services.Interfaces.Tokens;
using IdentityApi.Services.Interfaces.Users;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApiCore.Logic.Base.Interfaces;
using WebApiCore.Logic.Base.Services;

namespace IdentityApi.Infrastructure.Startups
{
    public static class ServicesStartUp
    {
        public static IServiceCollection TryAddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IAuthService, AuthService>();
            services.TryAddScoped<IImageService, ImageService>();
            services.TryAddScoped<IRoleService, RoleService>();
            services.TryAddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
