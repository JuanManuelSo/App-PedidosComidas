using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemPedido
    {
        public int Id_itemPedido { get; set; }
        public int Fk_id_pedido { get; set; }
        public int Fk_id_producto { get; set; }
        public int Cantidad { get; set; }
        public float Precio_unitario { get; set; }

        // Relaciones
        public Pedido Pedido { get; set; } 
        public Producto Producto { get; set; } 
    }
}

