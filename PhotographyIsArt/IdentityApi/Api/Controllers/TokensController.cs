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

		public TokensController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		[HttpPost]
		[Route("refresh")]
		public async Task<IActionResult> RefreshTokenAsync()
		{
			var refreshToken = Request.Cookies[SpecialConstants.RefreshTokenCookieName];
			var user = await _tokenService.GetUserByTokenAsync(refreshToken);
			_tokenService.RefreshTokenCheck(refreshToken, user);
			var token = await _tokenService.TokenSetupAsync(user, Response);
			return Ok(token);
		}
	}
}
