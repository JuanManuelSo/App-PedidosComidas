using Domain.Entities;
using Domain.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int MetodoPagoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public EstadoPago EstadoPago { get; set; }  = EstadoPago.Pendiente;

        public static PagoDto CreatePago(Pago pago)
        {
            var dto = new PagoDto();
            dto.Id = pago.Id;
            dto.PedidoId = pago.PedidoId;
            dto.MetodoPagoId = pago.MetodoPagoId;
            dto.Fecha = pago.Fecha;
            dto.EstadoPago = pago.EstadoPago;
            return dto;
        }

        public static List<PagoDto> CreateList(List<Pago> pagoList)
        {
            var dtoList = new List<PagoDto>();
            foreach (var p in pagoList)
            {
                dtoList.Add(CreatePago(p));
            }
            return dtoList;
        }
    }

    public class MetodoPagoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string TipoMetodo { get; set; }
        public string Detalles { get; set; }

        public static MetodoPagoDto CreateMetodoPago(MetodoPago metodoPago)
        {
            var dto = new MetodoPagoDto();
            dto.Id = metodoPago.Id;
            dto.UsuarioId = metodoPago.UsuarioId;
            dto.TipoMetodo = metodoPago.TipoMetodo;
            dto.Detalles = metodoPago.Detalles;
            return dto;
        }
    }
}
