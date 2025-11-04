using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
       private readonly IUsuarioService _usuarioRepository;
        public UsuarioService(IUsuarioService usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuario con id:{id} no fue encontrado.");
            }
            return usuario;
        }
    }
}
