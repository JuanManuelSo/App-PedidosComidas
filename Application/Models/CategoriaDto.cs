using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;      

namespace Application.Models
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre_categoria { get; set; }

        public static CategoriaDto CreateCategoria(Categoria categoria)
        {
            var dto = new CategoriaDto();
            dto.Id = categoria.Id;
            dto.Nombre_categoria = categoria.Nombre_categoria;
            return dto;
        }
    }
}
