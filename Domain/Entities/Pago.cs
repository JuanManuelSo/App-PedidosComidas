using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pago
    {
        public int Id { get; set; }
        public int pedidoId { get; set; }
        public int metodoPagoId { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoPago estado_pago { get; set; }

        // Relaciones
        public Pedido Pedido { get; set; } = null!;
        public MetodoPago MetodoPago { get; set; } = null!;

    }
}
