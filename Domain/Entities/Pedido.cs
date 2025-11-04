using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int usuarioId { get; set; }
        public string Direccion { get; set; }
        public string Tiempo_estimado { get; set; }

        public decimal Precio_total { get; set; }
        public EstadoPedido Estado_pedido { get; set; } 

        // Relaciones
        public Usuario Usuario { get; set; } 
        public ICollection<ItemPedido> ItemsPedido { get; set; } = new List<ItemPedido>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
