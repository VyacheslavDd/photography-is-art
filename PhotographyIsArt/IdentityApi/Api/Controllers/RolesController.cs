using AutoMapper;
using IdentityApi.Api.Controllers.PostModels.Roles;
using IdentityApi.Api.Controllers.ViewModels.Roles;
using IdentityApi.Domain.Entities;
using IdentityApi.Services.Interfaces.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;

		public RolesController(IRoleService roleService, IMapper mapper)
		{
			_roleService = roleService;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("all")]
		[ProducesResponseType(typeof(GetRoleResponse), 200)]
		public async Task<IActionResult> GetRolesAsync()
		{
			var roles = await _roleService.GetAllAsync();
			var responseRoles = _mapper.Map<List<GetRoleResponse>>(roles);
			return Ok(responseRoles);
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(typeof(GetRoleResponse), 200)]
		public async Task<IActionResult> GetRoleAsync([FromRoute] Guid id)
		{
			var role = await _roleService.GetByGuidAsync(id);
			var responseRole = _mapper.Map<GetRoleResponse>(role);
			return Ok(responseRole);
		}

		[HttpDelete]
		[Route("{id}/delete")]
		public async Task<IActionResult> DeleteRolesAsync([FromRoute] Guid id)
		{
			await _roleService.DeleteAsync(id);
			return Ok();
		}

		[HttpPost]
		[Route("create")]
		[ProducesResponseType(typeof(CreateRoleResponse), 201)]
		public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest roleRequest)
		{
			var role = _mapper.Map<Role>(roleRequest);
			var guid = await _roleService.AddAsync(role);
			return Ok(new CreateRoleResponse() { Id = guid });
		}

		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> UpdateRoleAsync([FromRoute] Guid id, [FromBody] UpdateRoleRequest request)
		{
			var role = _mapper.Map<Role>(request);
			await _roleService.UpdateAsync(role, id);
			return Ok();
		}

		[HttpPost]
		[Route("users/{userId}/add/{roleId}")]
		public async Task<IActionResult> AddRoleToUserAsync([FromRoute] Guid userId, [FromRoute] Guid roleId)
		{
			await _roleService.AssignRoleToUserAsync(roleId, userId);
			return Ok();
		}

		[HttpDelete]
		[Route("users/{userId}/delete/{roleId}")]
		public async Task<IActionResult> RemoveRoleFromUserAsync([FromRoute] Guid userId, [FromRoute] Guid roleId)
		{
			await _roleService.RemoveRoleFromUserAsync(roleId, userId);
			return Ok();
		}
	}
}
