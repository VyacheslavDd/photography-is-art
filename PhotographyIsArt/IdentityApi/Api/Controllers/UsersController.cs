using AutoMapper;
using IdentityApi.Api.Controllers.PostModels.Users;
using IdentityApi.Api.Controllers.ViewModels.Roles;
using IdentityApi.Api.Controllers.ViewModels.Users;
using IdentityApi.Domain.Entities;
using IdentityApi.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Api.Controllers
{
	/// <summary>
	/// Нужно авторизоваться, чтобы пользоваться этим контроллером
	/// </summary>
    [Route("api/[controller]")]
	[ApiController]
	[Authorize]
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request">Дату вводить в формате гггг-мм-дд; телефон с +7 или 8</param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateUserRequest request)
		{
			var userModel = _mapper.Map<User>(request);
			var entity = await _userService.GetByGuidAsync(id);
			if (userModel.Email.ToLower() != entity.Email.ToLower() || userModel.Login.ToLower() != entity.Login.ToLower()
				|| userModel.Phone != entity.Phone)
				await _authService.CheckUserNonExistenceAsync(userModel);
			await _userService.UpdateAsync(userModel, id, request.ProfilePicture);
			return Ok();
		}

		[HttpGet]
		[Route("all")]
		[ProducesResponseType(typeof(GetShortUserResponse), 200)]
		public async Task<IActionResult> GetUsersAsync()
		{
			var users = await _userService.GetAllAsync();
			var responseModels = _mapper.Map<List<GetShortUserResponse>>(users);
			return Ok(responseModels);
		}

		/// <summary>
		/// Получение полной информации о пользователе
		/// </summary>
		[HttpGet]
		[Route("{id}/full")]
		[ProducesResponseType(typeof(GetFullUserResponse), 200)]
		public async Task<IActionResult> GetFullUserInfoAsync([FromRoute] Guid id)
		{
			var user = await _userService.GetByGuidAsync(id);
			var response = _mapper.Map<GetFullUserResponse>(user);
			response.Roles = _mapper.Map<List<GetRoleResponse>>(user.Roles);
			return Ok(response);
		}

		/// <summary>
		/// Получение краткой информации о пользователе
		/// </summary>
		[HttpGet]
		[Route("{id}/short")]
		[ProducesResponseType(typeof(GetShortUserResponse), 200)]
		public async Task<IActionResult> GetShortUserInfoAsync([FromRoute] Guid id)
		{
			var user = await _userService.GetByGuidAsync(id);
			var response = _mapper.Map<GetShortUserResponse>(user);
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
