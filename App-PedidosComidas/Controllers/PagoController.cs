using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Exceptions;


namespace App_PedidosComidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;
        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPagos()
        {
            try
            {
                var pagos = await _pagoService.GetAllPagos();
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePago([FromBody] CreationPagoDto creationPagoDto)
        {
            try
            {
                var pago = await _pagoService.CreatePago(creationPagoDto);
                return CreatedAtAction(nameof(GetAllPagos), new { id = pago.Id }, pago);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPagoById(int id)
        {
            try
            {
                var pago = await _pagoService.GetPagoById(id);
                return Ok(pago);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("ByPedido/{pedidoId}")]
        public async Task<IActionResult> GetPagosByPedidoId(int pedidoId)
        {
            try
            {
                var pagos = await _pagoService.GetPagosByPedidoId(pedidoId);
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("ByUsuario/{usuarioId}")]
        public async Task<IActionResult> GetPagosByUsuarioId(int usuarioId)
        {
            try
            {
                var pagos = await _pagoService.GetPagosByUsuarioId(usuarioId);
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePago(int id, [FromBody] CreationPagoDto creationPagoDto)
        {
            try
            {
                await _pagoService.UpdatePago(id, creationPagoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            try
            {
                await _pagoService.DeletePago(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}