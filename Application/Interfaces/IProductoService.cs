using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface IProductoService
    {
        Task<ProductoDto> GetProductoByIdAsync(int id);
        Task<IEnumerable<ProductoDto>> GetAllProductosAsync();
        Task<ProductoDto> CreateProducto(ProductoDto producto);
        Task UpdateProducto(int id, ProductoDto producto);
        Task DeleteProducto(ProductoDto producto);
    }
}
