using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Logic.Base.Interfaces
{
	public interface IImageService
	{
		public string CreateName(string fileName);
		public Task SaveFileAsync(IFormFile file, string root, string directory, string filePath);
		public void DeleteFile(string root, string directory, string fileName);
		public Task<byte[]> GetFileAsByteArrayAsync(string root, string directory, string fileName);
	}
}
