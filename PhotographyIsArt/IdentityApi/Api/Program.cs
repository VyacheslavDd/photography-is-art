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
using IdentityApi.Infrastructure.Startups;

var builder = WebApplication.CreateBuilder(args);
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.TryAddApi();
builder.Services.TryAddDomain(builder.Configuration);
builder.Services.TryAddServices();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(IdentityServerMapper)));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(RegistrationRequestValidator));

builder.Services.AddEndpointsApiExplorer().AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen(setup =>
{
	setup.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});
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
