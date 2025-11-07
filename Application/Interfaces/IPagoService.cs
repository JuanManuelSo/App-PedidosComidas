using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Request;
using Domain.Enum;


namespace Application.Interfaces
{
    public interface IPagoService
    {
        Task<PagoDto?> GetPagoById(int id);
        Task<IEnumerable<PagoDto>> GetAllPagos();
        Task <PagoDto> CreatePago(CreationPagoDto pago);  
        Task UpdatePago(int id, CreationPagoDto pago);
        Task DeletePago(int id);
        Task<IEnumerable<PagoDto>> GetPagosByPedidoId(int pedidoId);
        Task<IEnumerable<PagoDto>> GetPagosByUsuarioId(int usuarioId);
    }
}
