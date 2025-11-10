using Application.Models;
using Application.Models.Request;
using Domain.DTOs;
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
        Task<UsuarioDto?> GetUsuarioById(int id);
        List<UsuarioDto> GetAllUsuarios();
        Task<UsuarioDto> CreateUsuario(CreationUserDto creationuserDto);
        Task UpdateUsuario(int id, CreationUserDto creationuserDto);
        Task DeleteUsuario(int id);
        Usuario AuthenticateRepository(CredentialsDtoRequest credentials);
    }
}
