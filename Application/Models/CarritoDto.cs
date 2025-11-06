
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
        public int UsuarioId { get; set; }
        public List<ItemCarritoDto> Items { get; set; } = new();

        public static CarritoDto CreateCarrito(CarritoDto carrito)
        {
            var dto = new CarritoDto();
            dto.Id = carrito.Id;
            dto.UsuarioId = carrito.UsuarioId;
            dto.Items = carrito.Items;
            return dto;
        }

        public static List<CarritoDto> CreateList(List<CarritoDto> carritoList)
        {
            var dtoList = new List<CarritoDto>();
            foreach (var c in carritoList)
            {
                dtoList.Add(CreateCarrito(c));
            }
            return dtoList;
        }
    }

    public class ItemCarritoDto
    {
        public int Id { get; set; }
        public int CarritoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public static ItemCarritoDto CreateItemCarrito(ItemCarritoDto item)
        {
            var dto = new ItemCarritoDto();
            dto.Id = item.Id;
            dto.CarritoId = item.CarritoId;
            dto.ProductoId = item.ProductoId;
            dto.Cantidad = item.Cantidad;
            return dto;
        }
    }
}
