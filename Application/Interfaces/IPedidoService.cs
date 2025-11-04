using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoDto> GetPedidoByIdAsync(int id);
        Task<IEnumerable<PedidoDto>> GetAllPedidosAsync();
        Task<PedidoDto> CreatePedido(PedidoDto pedido);
        Task UpdatePedido(int id,PedidoDto pedido);
        Task DeletePedido(PedidoDto pedido);
        Task <IEnumerable<PedidoDto>> GetPedidosByUsuarioIdAsync(int usuarioId);
    }
}
