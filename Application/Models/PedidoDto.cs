using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int usuarioId { get; set; }
        public string Direccion { get; set; }
        public string TiempoEstimado { get; set; }

        public decimal PrecioTotal { get; set; }
        public EstadoPedido EstadoPedido { get; set; }

        public static PedidoDto CreatePedido(PedidoDto pedido)
        {
            var dto = new PedidoDto();
            dto.Id = pedido.Id;
            dto.usuarioId = pedido.usuarioId;
            dto.Direccion = pedido.Direccion;
            dto.TiempoEstimado = pedido.TiempoEstimado;
            dto.PrecioTotal = pedido.PrecioTotal;
            dto.EstadoPedido = pedido.EstadoPedido;
            return dto;
        }
    }

    public class ItemPedidoDto
    {
        public int Id { get; set; }
        public int pedidoId { get; set; }
        public int productoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_unitario { get; set; }

        
    }


}
