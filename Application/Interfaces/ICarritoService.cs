using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Request;

namespace Application.Interfaces
{
    public interface ICarritoService
    {
        Task<CarritoDto?> GetCarritoById(int id);
        Task<IEnumerable<CarritoDto>> GetAllCarritos();
        Task<CarritoDto> CreateCarrito(CreationCarritoDto creationCarritoDto);
        Task UpdateCarrito(int id,CarritoDto carrito);
        Task DeleteCarrito(int id);
        Task <CarritoDto> GetCarritoByUsuarioId(int usuarioId);

    }
}
