using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int pedidoId { get; set; }
        public int productoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_unitario { get; set; }

        // Relaciones
        public Pedido Pedido { get; set; } 
        public Producto Producto { get; set; } 
    }
}

