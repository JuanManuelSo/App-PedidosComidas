using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Models.Request;

namespace App_PedidosComidas.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class MetodoPagoController : ControllerBase
        {
            private readonly IMetodoPagoRepository _metodoPagoRepository;

            public MetodoPagoController(IMetodoPagoRepository metodoPagoRepository)
            {
                _metodoPagoRepository = metodoPagoRepository;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<MetodoPago>>> GetAll()
            {
                var metodosPago = await _metodoPagoRepository.GetAllAsync();
                return Ok(metodosPago);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<MetodoPago>> GetById(int id)
            {
                var metodoPago = await _metodoPagoRepository.GetByIdAsync(id);

                if (metodoPago == null)
                {
                    return NotFound();
                }

                return Ok(metodoPago);
            }

            [HttpGet("usuario/{usuarioId}")]
            public async Task<ActionResult<IEnumerable<MetodoPago>>> GetByUsuarioId(int usuarioId)
            {
                var metodosPago = await _metodoPagoRepository.GetMetodoPagoByUserIdAsync(usuarioId);
                return Ok(metodosPago);
            }

            [HttpPost]
            public async Task<ActionResult<MetodoPago>> Create([FromBody] CreationMetodoPagoDto createDto)
            {
                var metodoPago = new MetodoPago
                {
                    UsuarioId = createDto.UsuarioId,
                    TipoMetodo = createDto.TipoMetodo,
                    Detalles = createDto.Detalles
                };

                var created = await _metodoPagoRepository.CreateAsync(metodoPago);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            

           
        }
    
}


