using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Producto>> GetAllAsync()
        {
            return await _dbContext.Productos
                .Include(p => p.Categoria)
                .ToListAsync();
        }
        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _dbContext.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Producto> CreateAsync(Producto entity)
        {
            await _dbContext.Productos.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Producto entity)
        {
            _dbContext.Productos.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Producto entity)
        {
            _dbContext.Productos.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Producto>> GetByNombreAsync(string? Nombre)
        {
            return await _dbContext.Productos
                .Where(p => p.Nombre.Contains(Nombre))
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<List<Producto>> GetByCategoryAsync(int? categoriaId)
        {
            return await _dbContext.Productos
                .Where(p => p.CategoriaId == categoriaId)
                .Include(p => p.Categoria)
                .ToListAsync();
        }
    }
}