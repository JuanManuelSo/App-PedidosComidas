using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Carrito
    {
        public int Id_carrito { get; set; }
        public int Fk_id_usuario { get; set; }


        // Relaciones
        public Usuario Usuario { get; set; } 
        public ICollection<ItemCarrito> Items { get; set; } = new List<ItemCarrito>();

    }
}
