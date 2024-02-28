using AlbumApi.Dal;
using AlbumApi.Dal.Albums;
using AlbumApi.Dal.Albums.Interfaces;
using AlbumApi.Dal.Albums.Models;
using AlbumApi.Dal.Tags;
using AlbumApi.Dal.Tags.Interfaces;
using AlbumApi.Dal.Tags.Models;
using AlbumApi.Logic.Albums;
using AlbumApi.Logic.Albums.Interfaces;
using AlbumApi.Logic.Mappers;
using AlbumApi.Logic.Tags;
using AlbumApi.Logic.Tags.Interfaces;
using AlbumApi.Logic.Validators.Albums;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApiCore.Dal.Base.Interfaces;
using WebApiCore.Logic.Base.Interfaces;
using WebApiCore.Logic.Base.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(config => config.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AlbumDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));

builder.Services.AddScoped<IRepository<AlbumDal>, AlbumRepository>();
builder.Services.AddScoped<IRepository<AlbumTagDal>, AlbumTagRepository>();

builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IAlbumTagService, AlbumTagService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateAlbumRequestValidator));
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AlbumApiMapper)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
