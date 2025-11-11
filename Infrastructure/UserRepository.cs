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
    public class UserRepository : RepositoryBase<Usuario>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario?> GetUserByPhoneAsync(string telefono)
        {
            return await _dbContext.Usuarios

                .FirstOrDefaultAsync(u => u.Telefono == telefono);
        }

        
    }
}
