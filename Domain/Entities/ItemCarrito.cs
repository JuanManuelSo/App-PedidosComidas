using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemCarrito
    {
        public int Id_itemCarrito { get; set; }
        public int Fk_id_carrito { get; set; }
        public int Fk_id_producto { get; set; }

        public int Cantidad { get; set; }

        // Relaciones
        public Carrito Carrito { get; set; } = null!;
        public Producto Producto { get; set; } = null!;

    }
}

