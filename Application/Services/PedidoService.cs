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
        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<PedidoDto>> GetAllPedidos(int usuarioId, EstadoPedido estadoPedido)
        {
            // Obtener todos los pedidos
            var pedidos = await _pedidoRepository.GetAllAsync();

            // Filtrar por usuarioId y estadoPedido
            var pedidosFiltrados = pedidos
                .Where(p => p.usuarioId == usuarioId && p.EstadoPedido == estadoPedido)
                .Select(p => new PedidoDto
                {
                    Id = p.Id,
                    usuarioId = p.usuarioId,
                    Direccion = p.Direccion,
                    TiempoEstimado = p.TiempoEstimado,
                    PrecioTotal = p.PrecioTotal,
                    EstadoPedido = p.EstadoPedido
                    // Agrega otros campos necesarios del DTO aquí
                });

            return pedidosFiltrados;
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

        public async Task<PedidoDto> CreatePedido(CreationPedidoDto creationPedidoDto)
        {
            var newPedido = new Pedido();
            newPedido.usuarioId = creationPedidoDto.usuarioId;
            newPedido.Direccion = creationPedidoDto.Direccion;
            newPedido.TiempoEstimado = creationPedidoDto.TiempoEstimado;
            newPedido.PrecioTotal = creationPedidoDto.PrecioTotal;
            newPedido.EstadoPedido = creationPedidoDto.EstadoPedido;
            var createdPedido = await _pedidoRepository.CreateAsync(newPedido);
            return PedidoDto.CreatePedido(createdPedido);
        }
    }
