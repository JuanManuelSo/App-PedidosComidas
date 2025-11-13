using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreationPagoDto
    {
        [Required (ErrorMessage = "El Id del pedido es requerido")]
        public int PedidoId { get; set; }
        [Required (ErrorMessage = "El Id del metodo de pago es requerido")]
        public int MetodoPagoId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public EstadoPago EstadoPago { get; set; } = EstadoPago.Pendiente;
    }

    public class CreationMetodoPagoDto
    {
        [Required (ErrorMessage = "El Id del usuario es requerido")]
        public int UsuarioId { get; set; }
        [Required (ErrorMessage = "El tipo de metodo de pago es requerido")]
        public string TipoMetodo { get; set; } = string.Empty;
        [Required (ErrorMessage = "Los detalles del metodo de pago son requeridos")]
        public string Detalles { get; set; } = string.Empty;
    }

    public class UpdateEstadoPagoDto
    {
        [Required(ErrorMessage = "El estado del pago es requerido")]
        public EstadoPago EstadoPago { get; set; }
    }
}
