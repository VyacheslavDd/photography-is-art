﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Dal.Constants
{
	public static class SpecialConstants
	{
		public static string UploadsDirectoryName { get; set; } = "Uploads";
		public static string AlbumCoversDirectoryName { get; set; } = "Covers";
		public static string AlbumPicturesDirectoryName { get; set; } = "AlbumPictures";
		public static string UserProfilePicturesDirectoryName { get; set; } = "ProfilePictures";
		public static string RefreshTokenCookieName { get; set; } = "refreshToken";
		public static string TokenSectionFullName { get; set; } = "AppSettings:Token";
		public static int TokenExpireMinutes { get; set; } = 60;
	}
}
