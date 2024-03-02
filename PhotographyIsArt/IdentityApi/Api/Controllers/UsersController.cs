using AutoMapper;
using IdentityApi.Api.Controllers.PostModels.Users;
using IdentityApi.Api.Controllers.ViewModels.Users;
using IdentityApi.Domain.Entities;
using IdentityApi.Services.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public UsersController(IUserService userService, IAuthService authService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
			_authService = authService;
		}

		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateUserRequest request)
		{
			var userModel = _mapper.Map<User>(request);
			var entity = await _userService.GetByGuidAsync(id);
			if (entity.Email.ToLower() != userModel.Email.ToLower() || entity.Login.ToLower() != userModel.Login.ToLower())
				await _authService.CheckUserNonExistence(userModel);
			await _userService.UpdateAsync(userModel, id, request.ProfilePicture);
			return Ok();
		}

		[HttpGet]
		[Route("all")]
		[ProducesResponseType(typeof(GetUsersResponse), 200)]
		public async Task<IActionResult> GetUsersAsync()
		{
			var users = await _userService.GetAllAsync();
			var responseModels = _mapper.Map<List<GetUsersResponse>>(users);
			return Ok(responseModels);
		}
		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(typeof(GetUserResponse), 200)]
		public async Task<IActionResult> GetUserAsync([FromRoute] Guid id)
		{
			var user = await _userService.GetByGuidAsync(id);
			var response = _mapper.Map<GetUserResponse>(user);
			return Ok(response);
		}

		[HttpDelete]
		[Route("{id}/delete")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			await _userService.DeleteAsync(id);
			return Ok();
		}
	}
}
