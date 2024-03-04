using IdentityApi.Domain.Interfaces;
using IdentityApi.Services.Interfaces.Tokens;
using IdentityApi.Services.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Dal.Constants;

namespace IdentityApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TokensController : ControllerBase
	{
		private readonly ITokenService _tokenService;
		private readonly IAuthService _authService;

		public TokensController(ITokenService tokenService,  IAuthService authService)
		{
			_tokenService = tokenService;
			_authService = authService;
		}

		[HttpPost]
		[Route("refresh")]
		public async Task<IActionResult> RefreshTokenAsync()
		{
			var refreshToken = Request.Cookies[SpecialConstants.RefreshTokenCookieName];
			var user = await _tokenService.GetUserByTokenAsync(refreshToken);
			if (!user.Token.Token.Equals(refreshToken)) return Unauthorized("Некорректный токен.");
			if (user.Token.Expires < DateTime.Now.ToUniversalTime()) return Unauthorized("Время действия токена истекло.");
			var token = await _authService.TokenSetupAsync(user, Response);
			return Ok(token);
		}
	}
}
