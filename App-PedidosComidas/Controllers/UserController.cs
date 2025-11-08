using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Interfaces;
using Domain.DTOs;

namespace App_PedidosComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public ActionResult<string> Autenticar([FromBody] CredentialsDtoRequest credentials)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var token = _authenticationService.Authenticate(credentials);

            if (token == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            return Ok(new { token });
        }
    }
}