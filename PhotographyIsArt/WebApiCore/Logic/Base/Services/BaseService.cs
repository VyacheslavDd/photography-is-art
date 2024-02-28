using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Dal.Base.Interfaces;
using WebApiCore.Logic.Base.Interfaces;

namespace WebApiCore.Logic.Base.Services
{
	//в будущем буду наследоваться от этого базового сервиса, при использовании Onion архитектуры, баз данных
	public abstract class BaseService<T> : IService<T> where T : class
	{
		private readonly IRepository<T> _repository;

		public BaseService(IRepository<T> repository)
		{
			_repository = repository;
		}

		public virtual async Task<Guid> AddAsync(T entity)
		{
			var guid = await _repository.AddAsync(entity);
			return guid;
		}

		public virtual async Task DeleteAsync(Guid guid)
		{
			await _repository.DeleteAsync(guid);
		}

		public virtual async Task<List<T?>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public virtual async Task<T?> GetByGuidAsync(Guid guid)
		{
			return await _repository.GetByGuidAsync(guid);
		}

		public virtual async Task UpdateAsync()
		{
			await _repository.UpdateAsync();
		}
	}
}
