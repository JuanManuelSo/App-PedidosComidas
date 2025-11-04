using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> GetUsuarioByIdAsync(int id);
        Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync();
        Task<UsuarioDto> CreateUsuario(UsuarioDto usuario);
        Task UpdateUsuario(UsuarioDto usuario);
        Task DeleteUsuario(UsuarioDto usuario);
    }
}
