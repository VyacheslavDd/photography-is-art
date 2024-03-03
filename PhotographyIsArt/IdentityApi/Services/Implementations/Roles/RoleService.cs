using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Services.Interfaces.Roles;
using WebApiCore.Logic.Base.Services;

namespace IdentityApi.Services.Implementations.Roles
{
	public class RoleService : BaseService<Role>, IRoleService
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IUserRepository _userRepository;

		public RoleService(IRoleRepository roleRepository, IUserRepository userRepository) : base(roleRepository)
		{
			this._roleRepository = roleRepository;
			_userRepository = userRepository;
		}

		public override async Task<Guid> AddAsync(Role role)
		{
			await ValidateRoleAsync(role);
			return await base.AddAsync(role);
		}

		public async Task AssignRoleToUserAsync(Guid roleId, Guid userId)
		{
			(Role role, User user) = await GetRoleAndUser(roleId, userId);
			if (user.Roles.Any(r => r.Name == role.Name)) throw new Exception("Данная роль уже добавлена пользователю!");
			role.Users.Add(user);
			user.Roles.Add(role);
			await base.UpdateAsync();
		}
		public async Task RemoveRoleFromUserAsync(Guid roleId, Guid userId)
		{
			(Role role, User user) = await GetRoleAndUser(roleId, userId);
			if (!user.Roles.Any(r => r.Name == role.Name)) throw new Exception("Указанная роль не существует у пользователя!");
			role.Users.Remove(user);
			user.Roles.Remove(role);
			await base.UpdateAsync();
		}

		private async Task<Tuple<Role, User>> GetRoleAndUser(Guid roleId, Guid userId)
		{
			var role = await _roleRepository.GetByGuidAsync(roleId);
			if (role is null) throw new Exception("Укажите существующую роль!");
			var user = await _userRepository.GetByGuidAsync(userId);
			if (user is null) throw new Exception("Указанный пользователь не существует!");
			return Tuple.Create(role, user);
		}

		public async Task<Role> GetDefaultRoleAsync()
		{
			return await _roleRepository.FindDefaultRoleAsync();
		}


		public async Task UpdateAsync(Role role, Guid id)
		{
			var entity = await _roleRepository.GetByGuidAsync(id);
			entity.Name = role.Name;
			entity.IsDefault = role.IsDefault;
			await ValidateRoleAsync(role);
			await base.UpdateAsync();
		}

		public async Task ValidateRoleAsync(Role role)
		{
			var dbRole = await _roleRepository.FindRoleByNameAsync(role.Name);
			if (dbRole is not null) throw new Exception("Данная роль уже существует!");
			var defaultRole = await GetDefaultRoleAsync();
			if (defaultRole is not null && role.IsDefault) throw new Exception("Невозможно добавить вторую роль по умолчанию!");
		}

	}
}
