using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Logic.Base.Interfaces;

namespace WebApiCore.Helpers
{
	public static class AlbumServiceHelper
	{
		public async static Task<string> SavePictures(List<IFormFile> files, IImageService service, string contentRootPath, string dirName)
		{
			var builder = new StringBuilder();
			foreach (var file in files)
			{
				var path = service.CreateName(file.FileName);
				builder.Append(path);
				builder.Append(' ');
				await service.SaveFileAsync(file, contentRootPath, dirName, path);
			}
			return builder.ToString();
		}

		public static void DeletePictures(string pictures, IImageService service, string contentRootPath, string dirName)
		{
			var files = pictures.Split();
			foreach (var file in files)
			{
				service.DeleteFile(contentRootPath, dirName, file);
			}
		}
	}
}
