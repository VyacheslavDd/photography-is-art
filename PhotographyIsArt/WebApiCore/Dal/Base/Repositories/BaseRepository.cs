using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Dal.Base.Interfaces;

namespace WebApiCore.Dal.Base.Repositories
{
    //в будущем буду наследоваться от этого базового репозитория
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly DbContext _context;

        public BaseRepository(DbSet<T> dbSet, DbContext context)
        {
            _dbSet = dbSet;
            _context = context;
        }

        public virtual async Task<Guid> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            var guid = (Guid)result.Property("Id").CurrentValue;
            return guid;
        }

        public virtual async Task DeleteAsync(Guid guid)
        {
            var entity = await GetByGuidAsync(guid);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByGuidAsync(Guid guid)
        {
            return await _dbSet.FindAsync(guid);
        }

        public virtual async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
