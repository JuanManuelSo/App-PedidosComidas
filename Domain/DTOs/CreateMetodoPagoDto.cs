//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.DTOs
//{
//    public class CreateMetodoPagoDto
//    {
//        [Required(ErrorMessage = "El ID de usuario es requerido")]
//        public int UsuarioId { get; set; }

//        [Required(ErrorMessage = "El tipo de método es requerido")]
//        [StringLength(50, ErrorMessage = "El tipo de método no puede exceder 50 caracteres")]
//        public string TipoMetodo { get; set; } 

//        [Required(ErrorMessage = "Los detalles son requeridos")]
//        [StringLength(200, ErrorMessage = "Los detalles no pueden exceder 200 caracteres")]
//        public string Detalles { get; set; } 
//    }
//}
