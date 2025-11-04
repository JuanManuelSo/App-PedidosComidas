using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaDto> GetCategoriaByIdAsync(int id);
        Task<IEnumerable<CategoriaDto>> GetAllCategoriasAsync();
        Task<CategoriaDto> CreateCategoria(CategoriaDto categoria);
        Task UpdateCategoria(int id, CategoriaDto categoria);
        Task DeleteCategoria(CategoriaDto categoria);

    }
}
