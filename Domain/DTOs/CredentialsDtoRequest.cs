//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.DTOs
//{
//    public class CredentialsDtoRequest
//    {

//        [Required(ErrorMessage = "El telefono es requerido")]
//        [MaxLength(10, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
//        public string Telefono { get; set; }
//        [Required(ErrorMessage = "La contraseña es requerida")]
//        public string Password { get; set; } = string.Empty;

//    }
//}
