using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class ProductoService : IProductoService
    {
        // Inyeccion de dependencias
        private readonly IProductoRepository _productoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductoService(IProductoRepository productoRepository, ICategoryRepository categoryRepository)
        {
            _productoRepository = productoRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductoDto> GetProductoById(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
            {
                throw new NotFoundException($"Producto con id:{id} no fue encontrado.");
            }
            return ProductoDto.CreateProducto(producto);
        }

        public async Task<IEnumerable<ProductoDto>> GetAllProductos(int? categoryId, string? Nombre)
        {
            //FALTAN FILTROS
            bool catIdFlag = (categoryId is null || categoryId < 1);
            bool nombreFlag = string.IsNullOrWhiteSpace(Nombre);

            if (catIdFlag && !nombreFlag)
            {
                // Solo filtro por nombre
                var productos = await _productoRepository.GetByNombreAsync(Nombre.Trim());
                if (!productos.Any())
                {
                    throw new NotFoundException($"No se encontró ningún producto que contenga: {Nombre.Trim()}");
                }
                return ProductoDto.CreateList(productos);
            }
            else if (!catIdFlag && nombreFlag)
            {
                // Solo filtro por categoría
                var productos = await _productoRepository.GetByCategoryAsync(categoryId.Value);
                if (!productos.Any())
                {
                    throw new NotFoundException($"No se encontró ningún producto en la categoría {categoryId}");
                }
                return ProductoDto.CreateList(productos);
            }
            else
            {
                // Filtro categoria + nombre
                var productos = await _productoRepository.GetByCategoryAsync(categoryId.Value);
                if (productos.Any())
                {
                    var productosFiltrados = productos
                        .Where(p => p.Nombre.Contains(Nombre.Trim(), StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (productosFiltrados.Any())
                    {
                        return ProductoDto.CreateList(productosFiltrados);
                    }
                    throw new NotFoundException($"No se encontró ningún producto que contenga: {Nombre}");
                }
                throw new NotFoundException($"No se encontró ningún producto en la categoría {categoryId}");
            }

           
        }
        public async Task<ProductoDto> CreateProducto(CreationProductoDto creationProductoDto)
        {
            var category = await _categoryRepository.GetByIdAsync(creationProductoDto.CategoriaId);

            if (category is null)
            {
                throw new ValidationException("El id de la categoría es inválido");
            }

            if (creationProductoDto.Precio <= 0)
            {
                throw new ValidationException("El precio del producto debe ser mayor a cero");
            }
            
            var newProducto = new Producto();

            newProducto.Nombre = creationProductoDto.Nombre;
            newProducto.Precio = creationProductoDto.Precio;
            newProducto.CategoriaId = creationProductoDto.CategoriaId;

            var createdProducto = await _productoRepository.CreateAsync(newProducto);

            return ProductoDto.CreateProducto(createdProducto);
        }

        public async Task UpdateProducto(int id, CreationProductoDto creationProductoDto)
        {
            var productoToUpdate = await _productoRepository.GetByIdAsync(id);
            if (productoToUpdate == null)
            {
                throw new NotFoundException($"Producto con id:{id} no fue encontrado.");
            }
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"Categoria con id:{id} no fue encontrada.");
            }
            productoToUpdate.Nombre = creationProductoDto.Nombre;
            productoToUpdate.Precio = creationProductoDto.Precio;
            productoToUpdate.CategoriaId = creationProductoDto.CategoriaId;

            await _productoRepository.UpdateAsync(productoToUpdate);
        }

        public async Task DeleteProducto(int id)
        {
            var existingProducto = await _productoRepository.GetByIdAsync(id);
            if (existingProducto == null)
            {
                throw new NotFoundException($"Producto con id:{id} no fue encontrado.");
            }
            await _productoRepository.DeleteAsync(id); //CHEQUEAR
        }
        public async Task<ProductoDto> GetProductoByName(string? nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre del producto no puede estar vacío.");
            }

            var productos = await _productoRepository.GetByNombreAsync(nombre.Trim());
            var producto = productos.FirstOrDefault();

            if (producto == null)
            {
                throw new NotFoundException($"Producto con nombre: {nombre.Trim()} no fue encontrado.");
            }

            return ProductoDto.CreateProducto(producto);
        }
    }
}
