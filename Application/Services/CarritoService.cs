using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUserRepository _usuarioRepository;

        public CarritoService(
            ICarritoRepository carritoRepository,
            IProductoRepository productoRepository,
            IUserRepository usuarioRepository)
        {
            _carritoRepository = carritoRepository;
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
        }


        public async Task<CarritoDto> GetCarritoByUsuarioId(int usuarioId)
        {
            var carrito = await _carritoRepository.GetByUserIdAsync(usuarioId);
            if (carrito == null)
            {
                throw new NotFoundException($"Carrito para el usuario con id:{usuarioId} no fue encontrado.");
            }
            return CarritoDto.CreateCarrito(carrito); 
        }

        public async Task<CarritoDto> CreateCarrito(CreationCarritoDto creationCarritoDto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(creationCarritoDto.UsuarioId);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuario con id:{creationCarritoDto.UsuarioId} no fue encontrado.");
            }
            var newCarrito = new Carrito();

            newCarrito.UsuarioId = creationCarritoDto.UsuarioId;
            newCarrito.Items = new List<ItemCarrito>();

            var createdCarrito = await _carritoRepository.CreateAsync(newCarrito);
            return CarritoDto.CreateCarrito(createdCarrito); 
        }

        public async Task DeleteCarrito(int id)
        {
            var carritoToDelete = await _carritoRepository.GetByIdAsync(id);
            if (carritoToDelete == null)
            {
                throw new NotFoundException($"Carrito con id:{id} no fue encontrado.");
            }
            await _carritoRepository.DeleteAsync(carritoToDelete);
        }

        public async Task<IEnumerable<CarritoDto>> GetAllCarritos()
        {
            var carritos = await _carritoRepository.GetAllAsync();
            return CarritoDto.CreateList(carritos);
        }

        public async Task<CarritoDto?> GetCarritoById(int id)
        {
            var carrito = await _carritoRepository.GetByIdAsync(id);
            if (carrito == null)
            {
                throw new NotFoundException($"Carrito con id:{id} no fue encontrado."); ;
            }
            return CarritoDto.CreateCarrito(carrito); 
        }

        public async Task UpdateCarrito(int id, CarritoDto carrito)
        {
            var carritoToUpdate = await _carritoRepository.GetByIdAsync(id);
            if (carritoToUpdate == null)
            {
                throw new NotFoundException($"Carrito con id:{id} no fue encontrado.");
            }
            await _carritoRepository.UpdateAsync(carritoToUpdate);
        }
    }
}
