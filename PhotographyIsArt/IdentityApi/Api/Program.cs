using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Infrastructure.Contexts;
using IdentityApi.Infrastructure.Repositories;
using IdentityApi.Infrastructure.Validators.Users;
using IdentityApi.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApiCore.Dal.Base.Interfaces;
using WebApiCore.Logic.Base.Interfaces;
using WebApiCore.Logic.Base.Services;
using IdentityApi.Services.Interfaces.Users;
using IdentityApi.Services.Implementations.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
	opt.SerializerSettings.Converters.Add(new StringEnumConverter
	{
		NamingStrategy = new CamelCaseNamingStrategy()
	});
})
	.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<UsersDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(IdentityServerMapper)));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(RegistrationRequestValidator));

builder.Services.AddEndpointsApiExplorer().AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
