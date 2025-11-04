using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;  

namespace Application.Models
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }

        public static ProductoDto CreateProducto(Producto producto)
        {
            var dto = new ProductoDto();
            dto.Id = producto.Id;
            dto.Nombre = producto.Nombre;
            dto.Precio = producto.Precio;
            dto.CategoriaId = producto.CategoriaId;
            return dto;

        }


    }
}
