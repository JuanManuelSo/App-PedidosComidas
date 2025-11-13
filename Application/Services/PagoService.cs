using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IMetodoPagoRepository _metodoPagoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public PagoService(
                IPagoRepository pagoRepository,
                IMetodoPagoRepository metodoPagoRepository,
                IPedidoRepository pedidoRepository)
        {
            _pagoRepository = pagoRepository;
            _metodoPagoRepository = metodoPagoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PagoDto?> GetPagoById(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            return pago != null ? PagoDto.CreatePago(pago) : null;
        }

            public async Task<IEnumerable<PagoDto>> GetAllPagos()
            {
                var pagos = await _pagoRepository.GetAllAsync();
                return PagoDto.CreateList(pagos.ToList());
            }

            public async Task<IEnumerable<PagoDto>> GetPagosByPedidoId(int pedidoId)
            {
                var pagos = await _pagoRepository.GetPagosByPedidoIdAsync(pedidoId);
                return PagoDto.CreateList(pagos.ToList());
            }

            public async Task<IEnumerable<PagoDto>> GetPagosByUsuarioId(int usuarioId)
            {
                var pagos = await _pagoRepository.GetPagosByUserIdAsync(usuarioId);
                return PagoDto.CreateList(pagos.ToList());
            }

        public async Task<PagoDto> CreatePago(CreationPagoDto creationPagoDto)
        {
            // Validar existencia de Pedido
            var pedido = await _pedidoRepository.GetByIdAsync(creationPagoDto.PedidoId);
            if (pedido == null)
                throw new NotFoundException($"Pedido con id {creationPagoDto.PedidoId} no encontrado.");

            // Validar existencia de Metodo de Pago
            var metodoPago = await _metodoPagoRepository.GetByIdAsync(creationPagoDto.MetodoPagoId);
            if (metodoPago == null)
                throw new NotFoundException($"Método de pago con id {creationPagoDto.MetodoPagoId} no encontrado.");

            var pago = new Pago
            {
                PedidoId = creationPagoDto.PedidoId,
                MetodoPagoId = creationPagoDto.MetodoPagoId,
                Fecha = creationPagoDto.Fecha,
                EstadoPago = creationPagoDto.EstadoPago
            };

            await _pagoRepository.CreateAsync(pago);

            return PagoDto.CreatePago(pago);
        }

        public async Task UpdatePago(int id, CreationPagoDto creationPagoDto)
        {
            var pagoToUpdate = await _pagoRepository.GetByIdAsync(id);
            if (pagoToUpdate == null)
                throw new NotFoundException($"No se encontro pago con id{id}");

            pagoToUpdate.EstadoPago = creationPagoDto.EstadoPago;

            await _pagoRepository.UpdateAsync(pagoToUpdate);

        }

 
            public async Task DeletePago(int id)
            {
                var pago = await _pagoRepository.GetByIdAsync(id);
                if (pago == null)
                    throw new NotFoundException($"No existe pago con id:{id}");

                if (pago.EstadoPago == EstadoPago.Completado)
                    throw new NotFoundException("No se puede eliminar un pago completado.");

                await _pagoRepository.DeleteAsync(pago);
            }

        
    }
}