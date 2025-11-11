using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        private readonly AppDbContext _dbContext;
        public PedidoRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Pedido>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Pedidos
                .Where(p => p.UsuarioId == userId)
                .Include(p => p.Usuario)
                .Include(p => p.Direccion)
                .Include(p => p.TiempoEstimado)
                .Include(p => p.PrecioTotal)
                .ToListAsync();
        }
                
    }
}
