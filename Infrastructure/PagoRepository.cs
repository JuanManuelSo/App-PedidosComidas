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
    public class PagoRepository : IPagoRepository
    {
        private readonly AppDbContext _dbContext;

        public PagoRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Pago> CreateAsync(Pago entity)
        {
            await _dbContext.Pagos.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Pago>> GetAllAsync()
        {
            return await _dbContext.Pagos
                .Include(p => p.Pedido)
                .Include(p => p.MetodoPago)
                .ToListAsync();
        }

        public async Task<Pago> GetByIdAsync(int id)
        {
            return await _dbContext.Pagos
                .Include(p => p.Pedido)
                .Include(p => p.MetodoPago)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Pago entity)
        {
            _dbContext.Pagos.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pago entity)
        {
                _dbContext.Pagos.Remove(entity);
                await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Pago>> GetPagosByUserIdAsync(int userId)
        {
            return await _dbContext.Pagos
                .Include(p => p.Pedido)
                .Include(p => p.MetodoPago)
                .Where(p => p.Pedido.UsuarioId == userId)
                .ToListAsync();
        }


    }
}
