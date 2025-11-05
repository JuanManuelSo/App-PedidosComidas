using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreationPedidoDto
    {
        [Required (ErrorMessage = "El Id del usuario es requerido")]
        public int usuarioId { get; set; }
        [Required(ErrorMessage = "La direccion del pedido requerida")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El Tiempo estimado del pedido es requerido")]
        public string TiempoEstimado { get; set; }
        [Required(ErrorMessage = "El Precio Total del pedido es requerido")]
        public decimal PrecioTotal { get; set; }
        [Required(ErrorMessage = "El Estado Pedido del pedido es requerido")]
        public EstadoPedido EstadoPedido { get; set; }
    }
}
