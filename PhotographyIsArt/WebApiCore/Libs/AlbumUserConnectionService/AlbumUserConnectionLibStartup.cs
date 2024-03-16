using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Libs.AlbumUserConnectionService.Interfaces;

namespace WebApiCore.Libs.AlbumUserConnectionService
{
	public static class AlbumUserConnectionLibStartup
	{
		public static IServiceCollection TryAddAlbumUserConnectionLib(this IServiceCollection services)
		{
			services.TryAddScoped<IAlbumUserConnectionService, AlbumUserConnectionService>();
			return services;
		}
	}
}
