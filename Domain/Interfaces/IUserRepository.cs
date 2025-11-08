using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Usuario? GetByNombre(string nombre);
        List<Usuario> GetAll();
        Usuario? GetById(int id);
        void Create(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(Usuario usuario);
        void Save();
    }
}
