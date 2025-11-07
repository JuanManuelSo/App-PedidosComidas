using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace App_PedidosComidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _carritoService;
        public CarritoController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarritoById(int id)
        {
            try
            {
                var carrito = await _carritoService.GetCarritoById(id);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetCarritoByUsuarioId(int usuarioId)
        {
            try
            {
                var carrito = await _carritoService.GetCarritoByUsuarioId(usuarioId);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarrito([FromBody] CreationCarritoDto creationCarritoDto)
        {
            try
            {
                var carrito = await _carritoService.CreateCarrito(creationCarritoDto);
                return CreatedAtAction(nameof(GetCarritoById), new { id = carrito.Id }, carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("{carritoId}/items")]
        public async Task<IActionResult> AddItemToCarrito(int carritoId, [FromBody] ItemCarritoDto itemCarritoDto)
        {
            try
            {
                var carrito = await _carritoService.AddItemToCarrito(carritoId, itemCarritoDto.ProductoId, itemCarritoDto.Cantidad);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{carritoId}/items/{productoId}")]
        public async Task<IActionResult> UpdateItemQuantity(int carritoId, int productoId, [FromBody] int nuevaCantidad)
        {
            try
            {
                var carrito = await _carritoService.UpdateItemQuantity(carritoId, productoId, nuevaCantidad);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{carritoId}/items/{productoId}")]
        public async Task<IActionResult> RemoveItemFromCarrito(int carritoId, int productoId)
        {
            try
            {
                var carrito = await _carritoService.RemoveItemFromCarrito(carritoId, productoId);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
    public class AddItemRequest
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }

    public class UpdateQuantityRequest
    {
        public int NuevaCantidad { get; set; }
    }
}
