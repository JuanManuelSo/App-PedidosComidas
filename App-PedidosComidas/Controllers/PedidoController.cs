using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace App_PedidosComidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {

        private readonly IPedidoService _pedidoService;
        private readonly ICurrencyService _currencyService;
        public PedidoController(IPedidoService pedidoService, ICurrencyService currencyService)
        {
            _pedidoService = pedidoService;
            _currencyService = currencyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoById(id);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] CreationPedidoDto creationPedidoDto)
        {
            try
            {
                var pedido = await _pedidoService.CreatePedido(creationPedidoDto);
                return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.Id }, pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.GetAllPedidos();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetPedidosByEstado([FromQuery] Domain.Enum.EstadoPedido estado)
        {
            try
            {
                var pedidos = await _pedidoService.GetPedidosByEstado(estado);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetPedidosByUsuarioId(int usuarioId)
        {
            try
            {
                var pedidos = await _pedidoService.GetPedidosByUsuarioId(usuarioId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEstadoPedido(int id, [FromBody] UpdateEstadoPedidoDto updateEstadoPedidoDto)
        {
            try
            {
                await _pedidoService.UpdateEstadoPedido(id, updateEstadoPedidoDto.EstadoPedido);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            try
            {
                await _pedidoService.DeletePedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutCarrito([FromBody] CheckoutRequest request)
        {
            try
            {
                var pedido = await _pedidoService.CreatePedidoFromCarrito(
                    request.CarritoId,
                    request.DireccionEntrega
                );

                return CreatedAtAction(
                    nameof(GetPedidoById),
                    new { id = pedido.Id },
                    pedido
                );
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/precio-usd")]
        public async Task<IActionResult> GetPedidoPriceInUsd(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoById(id);
                var precioTotalArs = pedido.Items.Sum(item => item.PrecioUnitario * item.Cantidad);

                var precioEnUsd = await _currencyService.ConvertArsToUsd(precioTotalArs);

                return Ok(new
                {
                    pedidoId = id,
                    precioARS = precioTotalArs,
                    precioUSD = Math.Round(precioEnUsd, 2),
                    tasaCambio = await _currencyService.GetExchangeRate("ARS", "USD")
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

