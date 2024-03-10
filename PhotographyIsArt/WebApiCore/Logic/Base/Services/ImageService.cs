using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Dal.Constants;
using WebApiCore.Logic.Base.Interfaces;

namespace WebApiCore.Logic.Base.Services
{
	public class ImageService : IImageService
	{
		public string CreateName(string fileName)
		{
			var uniqueName = Guid.NewGuid().ToString() + "_" + fileName;
			return uniqueName;
		}

		public async Task SaveFileAsync(IFormFile file, string root, string directory, string fileName)
		{
			var uploadsFolder = Path.Combine(root, SpecialConstants.UploadsDirectoryName);
			if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
			var targetFolder = Path.Combine(uploadsFolder, directory);
			if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);
			var filePath = Path.Combine(targetFolder, fileName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			};
		}

		public void DeleteFile(string root, string directory, string fileName)
		{
			if (fileName is null) return;
			var path = Path.Combine(root, SpecialConstants.UploadsDirectoryName, directory, fileName);
			if (File.Exists(path)) File.Delete(path);
		}

		public async Task<byte[]> GetFileAsByteArrayAsync(string root, string directory, string fileName)
		{
			var path = Path.Combine(root, SpecialConstants.UploadsDirectoryName, directory, fileName);
			if (File.Exists(path)) return await File.ReadAllBytesAsync(path);
			return new byte[0];
		}
	}
}
