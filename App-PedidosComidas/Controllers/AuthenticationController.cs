using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Request;

namespace App_PedidosComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationService _AuthenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _AuthenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsDto credentials)
        {
            var token = await _AuthenticationService.Authenticate(credentials);

            if (token == null)
                return Unauthorized("Teléfono o contraseña incorrectos");

            return Ok(new { Token = token });
        }
    }
}