using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string TipoMetodo { get; set; }
        public string Detalles { get; set; } 

        // Relaciones
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
