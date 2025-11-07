using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CategoriaRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoriaRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _dbContext.Categorias.FindAsync(id);
        }
        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _dbContext.Categorias
                .Include(c => c.Productos)
                .ToListAsync();
        }

        public async Task<Categoria> CreateAsync(Categoria entity)
        {
            _dbContext.Categorias.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Categoria entity)
        {
            _dbContext.Categorias.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Categoria entity)
        {
            _dbContext.Categorias.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Categoria?> GetCategoryByName(string name)
        {
            return await _dbContext.Categorias
                .FirstOrDefaultAsync(c => c.NombreCategoria == name);
        }
    }
}
