using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface ICarritoService
    {
        Task<CarritoDto?> GetCarritoByIdAsync(int id);
        Task<IEnumerable<CarritoDto>> GetAllCarritosAsync();
        Task<CarritoDto> CreateCarrito(CarritoDto carrito);
        Task UpdateCarrito(int id,CarritoDto carrito);
        Task DeleteCarrito(CarritoDto carrito);
    }
}
