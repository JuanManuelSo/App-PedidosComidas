using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario([FromBody] CreationUserDto creationUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioDto = await _usuarioService.CreateUsuario(creationUserDto);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioDto.Id }, usuarioDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int id)
        {
            var usuarioDto = await _usuarioService.GetUsuarioById(id);
            if (usuarioDto == null)
            {
                return NotFound();
            }
            return Ok(usuarioDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuarioDto = await _usuarioService.GetUsuarioById(id);
            if (usuarioDto == null)
            {
                return NotFound();
            }
            await _usuarioService.DeleteUsuario(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> UpdateUsuario(int id, [FromBody] CreationUserDto updateUserDto)
        {
            var existingUsuario = await _usuarioService.GetUsuarioById(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }
            await _usuarioService.UpdateUsuario(id, updateUserDto);
            return Ok(existingUsuario);
        }

    }
}