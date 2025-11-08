using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUserRepository _usuarioRepository;
        public PedidoService(
            IPedidoRepository pedidoRepository,
            IProductoRepository productoRepository,
            IUserRepository usuarioRepository)
        {
            _pedidoRepository = pedidoRepository;
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<PedidoDto> GetPedidoById(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
            {
                throw new NotFoundException($"Pedido con id:{id} no fue encontrado.");
            }
            return PedidoDto.CreatePedido(pedido);
        }

        public async Task<IEnumerable<PedidoDto>> GetAllPedidos()
        {
            var pedidos = await _pedidoRepository.GetAllAsync();
            return pedidos.Select(PedidoDto.CreatePedido).ToList();
        }

        public async Task<IEnumerable<PedidoDto>> GetPedidosByUsuarioId(int usuarioId)
        {
            var pedidos = await _pedidoRepository.GetByUserIdAsync(usuarioId);
            return PedidoDto.CreateList(pedidos);
        }
        public async Task<IEnumerable<PedidoDto>> GetPedidosByEstado(EstadoPedido estado)
        {
            var pedidos = await _pedidoRepository.GetAllAsync();
            var pedidosFiltrados = pedidos.Where(p => p.EstadoPedido == estado);
            return pedidosFiltrados.Select(PedidoDto.CreatePedido).ToList();
        }

        public async Task<PedidoDto> CreatePedido(CreationPedidoDto creationPedidoDto)
        {
            // Validar que el usuario existe
            var usuario = await _usuarioRepository.GetByIdAsync(creationPedidoDto.UsuarioId);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuario con id:{creationPedidoDto.UsuarioId} no existe.");
            }

            // Validar que los productos existen y calcular el precio total
            decimal precioTotal = 0;
            var itemsPedido = new List<ItemPedido>();

            foreach (var itemDto in creationPedidoDto.Items)
            {
                var producto = await _productoRepository.GetByIdAsync(itemDto.ProductoId);
                if (producto == null)
                {
                    throw new NotFoundException($"Producto con id:{itemDto.ProductoId} no existe.");
                }

                var itemPedido = new ItemPedido
                {
                    ProductoId = itemDto.ProductoId,
                    Cantidad = itemDto.Cantidad,
                    PrecioUnitario = producto.Precio
                };

                precioTotal += itemPedido.PrecioUnitario * itemPedido.Cantidad;
                itemsPedido.Add(itemPedido);
            }

            // Crear el pedido
            var newPedido = new Pedido
            {
                UsuarioId = creationPedidoDto.UsuarioId,
                Direccion = creationPedidoDto.Direccion,
                TiempoEstimado = "30-45 min",
                PrecioTotal = precioTotal,
                EstadoPedido = EstadoPedido.Pendiente, // Siempre empieza como Pendiente
                ItemsPedido = itemsPedido
            };

            // Guardar el pedido
            var createdPedido = await _pedidoRepository.CreateAsync(newPedido);
            return PedidoDto.CreatePedido(createdPedido);
        }

        public async Task UpdateEstadoPedido(int id, EstadoPedido nuevoEstado)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
            {
                throw new NotFoundException($"Pedido con id:{id} no fue encontrado.");
            }

            pedido.EstadoPedido = nuevoEstado;
            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task DeletePedido(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
            {
                throw new NotFoundException($"Pedido con id:{id} no fue encontrado.");
            }

            // Solo poder eliminar si el estado es Entregado o Cancelado
            if (pedido.EstadoPedido == EstadoPedido.Entregado ||
                pedido.EstadoPedido == EstadoPedido.Cancelado)
            {
                await _pedidoRepository.DeleteAsync(pedido);
            }
            else
            {
                throw new InvalidOperationException(
                    "Solo se pueden eliminar pedidos Entregados o Cancelados.");
            }
        }
    }
}
