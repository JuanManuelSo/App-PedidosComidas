using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<Usuario>
    {
        Task DeleteAsync(Usuario existingUsuario);
        Task<Usuario?> GetUserByIdAsync(int id);
        Task<List<Usuario>> GetUserByNumber();
    }   
}
