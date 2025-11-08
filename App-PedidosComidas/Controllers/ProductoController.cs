using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace App_PedidosComidas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;
        public ProductoController(IProductoService productoService, ICategoriaService categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductos(int? categoryId, string? Nombre)
        {
            try
            {
                var productos = await _productoService.GetAllProductos(categoryId, Nombre);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            try
            {
                var producto = await _productoService.GetProductoById(id);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] CreationProductoDto creationProductoDto)
        {
            try
            {
                var category = await _categoriaService.GetCategoriaById(creationProductoDto.CategoriaId);
                if (category == null)
                {
                    return BadRequest(new { message = "La categoría especificada no existe." });
                }
                var producto = await _productoService.CreateProducto(creationProductoDto);
                return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] CreationProductoDto creationProductoDto)
        {
            try
            {
                var category = await _categoriaService.GetCategoriaById(creationProductoDto.CategoriaId);
                if (category == null)
                {
                    return BadRequest(new { message = "La categoría especificada no existe." });
                }
                await _productoService.UpdateProducto(id, creationProductoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                await _productoService.DeleteProducto(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}