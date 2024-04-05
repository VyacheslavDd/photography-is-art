using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Libs.AlbumUserConnectionService.Common;
using WebApiCore.Libs.AlbumUserConnectionService.Http;
using WebApiCore.Libs.AlbumUserConnectionService.Interfaces;
using WebApiCore.Libs.AlbumUserConnectionService.RPC;

namespace WebApiCore.Libs.AlbumUserConnectionService
{
	public static class AlbumUserConnectionLibStartup
	{
		public static IServiceCollection TryAddAlbumUserConnectionLib(this IServiceCollection services)
		{
			services.TryAddScoped<IHttpAlbumUserConnectionService, HttpAlbumUserConnectionService>();
			services.TryAddScoped<IRPCAlbumUserConnectionService, RPCAlbumUserConnectionService>();
			services.TryAddScoped<IAlbumUserConnectionService, AlbumUserConnectionService>();
			return services;
		}
	}
}
