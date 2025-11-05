using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUserRepository _usuarioRepository;
        public UsuarioService(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioDto?> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                throw new NotFoundException($"Usuario con id:{id} no fue encontrado.");
            }

            return UsuarioDto.CreateUser(usuario);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return UsuarioDto.CreateList(usuarios);
        }

        public async Task<UsuarioDto> CreateUsuario(CreationUserDto creationuserDto)
        {
            var newUsuario = new Usuario();

            newUsuario.Nombre = creationuserDto.Nombre;
            newUsuario.Contraseña = creationuserDto.Contraseña; // Hashear esto
            newUsuario.Apellido = creationuserDto.Apellido;
            newUsuario.Telefono = creationuserDto.Telefono;
            newUsuario.Rol = creationuserDto.Rol;

            var createdUsuario = await _usuarioRepository.CreateAsync(newUsuario);
            return UsuarioDto.CreateUser(createdUsuario);
        }

        public async Task UpdateUsuario(int id, CreationUserDto creationUserDto)
        {
            var usuarioToUpdate = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioToUpdate == null)
            {
                throw new NotFoundException($"Usuario con id:{id} no fue encontrado.");
            }

            usuarioToUpdate.Nombre = creationUserDto.Nombre;
            usuarioToUpdate.Apellido = creationUserDto.Apellido;
            usuarioToUpdate.Telefono = creationUserDto.Telefono;
            usuarioToUpdate.Rol = creationUserDto.Rol;

            await _usuarioRepository.UpdateAsync(usuarioToUpdate);
        }

        public async Task DeleteUsuario(int id)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                throw new NotFoundException($"Usuario con id:{id} no fue encontrado.");
            }
            await _usuarioRepository.DeleteAsync(existingUsuario);
        }
    }
}
