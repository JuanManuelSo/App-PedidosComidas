using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Entities;
using Application.Models.Request;

namespace App_PedidosComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUsuarioService _usuarioService;

        public UserController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = _usuarioService.GetAllUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        public ActionResult<UsuarioDto> CreateUsuario([FromBody] CreationUserDto creationUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuarioDto = _usuarioService.CreateUsuario(creationUserDto).Result;
            return CreatedAtAction(nameof(_usuarioService.GetUsuarioById), new { id = usuarioDto.Id }, usuarioDto); // asi anda despues ver si se pude simplificar
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioDto> GetUsuarioById(int id)
        {
            var usuarioDto = _usuarioService.GetUsuarioById(id);
            if (usuarioDto == null)
            {
                return NotFound();
            }
            return Ok(usuarioDto);
        }

        [HttpDelete]
        public ActionResult DeleteUsuario(int id)
        {
            var usuarioDto = _usuarioService.GetUsuarioById(id);
            if (usuarioDto == null)
            {
                return NotFound();
            }
            _usuarioService.DeleteUsuario(id);
            return NoContent();
        }

        [HttpPut]
        public ActionResult<UsuarioDto> UpdateUsuario(int id, [FromBody] CreationUserDto updateUserDto)
        {
        
            var existingUsuario = _usuarioService.GetUsuarioById(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }
            var updatedUsuario = _usuarioService.UpdateUsuario(id, updateUserDto);
            return Ok(updatedUsuario);
        }

        [HttpPost("login")]
        public ActionResult<string> Autenticar([FromBody] CredentialsDtoRequest credentials)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var token = _usuarioService.AuthenticateRepository(credentials);

            if (token == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            return Ok(new { token });
        }
    }
}