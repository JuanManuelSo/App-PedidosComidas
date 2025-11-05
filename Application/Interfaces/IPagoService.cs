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
        Task<Pago?> GetPagoById(int id);
        Task<IEnumerable<Pago>> GetAllPagos();
        Task <Pago> CreatePago(Pago pago);  
        Task UpdatePago(Pago pago);
        Task DeletePago(int id);
    }
}
