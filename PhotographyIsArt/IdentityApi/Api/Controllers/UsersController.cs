using AutoMapper;
using IdentityApi.Api.Controllers.PostModels.Users;
using IdentityApi.Api.Controllers.ViewModels.Users;
using IdentityApi.Domain.Entities;
using IdentityApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _service;
		private readonly IMapper _mapper;

		public UsersController(IUserService service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("register")]
		[ProducesResponseType(typeof(CreateUserResponse), 201)]
		public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
		{
			var user = _mapper.Map<User>(request);
			var guid = await _service.AddAsync(user);
			return Ok(new CreateUserResponse() { Id = guid });
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
		{
			var data = _mapper.Map<User>(request);
			await _service.CheckUser(data);
			return Ok();
		}

		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateUserRequest request)
		{
			var userModel = _mapper.Map<User>(request);
			await _service.UpdateAsync(userModel, id, request.ProfilePicture);
			return Ok();
		}
	}
}
