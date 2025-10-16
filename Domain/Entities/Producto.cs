using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto
    {
        public int Id_producto { get; set; }
        public int Fk_id_categoria { get; set; }
        public string Nombre { get; set; } 
        public float Precio { get; set; }

        // Relaciones
        public Categoria Categoria { get; set; } = null!;
        public ICollection<ItemPedido> ItemsPedido { get; set; } = new List<ItemPedido>();
        public ICollection<ItemCarrito> ItemsCarrito { get; set; } = new List<ItemCarrito>();
    }
}
