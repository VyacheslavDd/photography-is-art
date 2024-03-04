using AutoMapper;
using IdentityApi.Api.Controllers.PostModels.Users;
using IdentityApi.Api.Controllers.ViewModels.Users;
using IdentityApi.Domain.Entities;
using IdentityApi.Services.Interfaces.Tokens;
using IdentityApi.Services.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _service;
		private readonly IMapper _mapper;

		public AuthController(IAuthService service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request">Дату вводить в формате гггг-мм-дд; телефон с +7 или 8</param>
		/// <returns></returns>
		[HttpPost]
		[Route("register")]
		[ProducesResponseType(typeof(CreateUserResponse), 201)]
		public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
		{
			var user = _mapper.Map<User>(request);
			var guid = await _service.RegisterUserAsync(user);
			return Ok(new CreateUserResponse() { Id = guid });
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
		{
			var data = _mapper.Map<User>(request);
			var token = await _service.UserLoginAsync(data, Response);
			return Ok(token);
		}
	}
}
