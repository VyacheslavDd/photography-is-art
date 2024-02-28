using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Dal.Base.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<List<T?>> GetAllAsync();
		Task<T?> GetByGuidAsync(Guid guid);
		Task<Guid> AddAsync(T entity);
		Task UpdateAsync();
		Task DeleteAsync(Guid guid);
	}
}
