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
        public int Id_pago { get; set; }
        public int Fk_id_pedido { get; set; }
        public int Fk_id_metodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoPago estado_pago { get; set; }

        // Relaciones
        public Pedido Pedido { get; set; } = null!;
        public MetodoPago MetodoPago { get; set; } = null!;

    }
}
