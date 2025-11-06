using Domain.Interfaces;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> 9b71f099b3b7a5a706ab4e59b347d3ff4008871f
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
<<<<<<< HEAD
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
=======
        private readonly AppDbContext _db_context;

        public RepositoryBase(AppDbContext dbContext)
        {
            _db_context = dbContext;
        }

        public async Task<List<T>> GetAllAsync() { 
            return await _db_context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db_context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            _db_context.Set<T>().Add(entity);
            await _db_context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _db_context.Set<T>().Update(entity);
            await _db_context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _db_context.Set<T>().Remove(entity);
                await _db_context.SaveChangesAsync();
            }
        }

>>>>>>> 9b71f099b3b7a5a706ab4e59b347d3ff4008871f
    }
}
