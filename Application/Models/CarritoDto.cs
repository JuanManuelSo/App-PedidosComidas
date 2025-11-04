using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CarritoDto
    {
        public int Id { get; set; }
        public int usuarioId { get; set; }
        public List<ItemCarritoDto> Items { get; set; } = new();

        public static CarritoDto CreateCarrito(CarritoDto carrito)
        {
            var dto = new CarritoDto();
            dto.Id = carrito.Id;
            dto.usuarioId = carrito.usuarioId;
            dto.Items = carrito.Items;
            return dto;
        }
    }

    public class ItemCarritoDto
    {
        public int Id { get; set; }
        public int carritoId { get; set; }
        public int productoId { get; set; }
        public int Cantidad { get; set; }
    }
}
