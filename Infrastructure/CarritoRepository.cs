using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly AppDbContext _dbContext;
        public CarritoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
