using Domain.Entities;
using Domain.DTOs;
namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> CreateAsync(Usuario user);
        Task UpdateAsync(Usuario user);
        Task DeleteAsync(int id);
        Usuario AuthenticateRepository(CredentialsDtoRequest credentials);
    }
}
