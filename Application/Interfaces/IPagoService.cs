using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagoService
    {
        Task<Pago?> GetPagoByIdAsync(int id);
        Task<IEnumerable<Pago>> GetAllPagosAsync();
        Task <Pago> CreatePago(Pago pago);  
        Task UpdatePago(Pago pago);
        Task DeletePago(Pago pago);
    }
}
