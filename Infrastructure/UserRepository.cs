using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _dbContext.Usuarios
                .Include(u => u.Pedidos)
                .Include(u => u.Carritos)
                .ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _dbContext.Usuarios
                .Include(u => u.Pedidos)
                .Include(u => u.Carritos)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> CreateAsync(Usuario entity)
        {
            await _dbContext.Usuarios.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Usuario entity)
        {
            _dbContext.Usuarios.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Usuario?> GetUserByPhoneAsync(string telefono)
        {
            return await _dbContext.Usuarios

                .FirstOrDefaultAsync(u => u.Telefono == telefono);
        }

        public async Task DeleteAsync(Usuario entity)
        {
            _dbContext.Usuarios.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
