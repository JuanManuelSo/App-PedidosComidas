using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreationUserDto
    {
        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del usuario es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La contraseña del usuario es requerida")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage ="El telefono del usuario es requerido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El rol del usuario es requerido")]

        public RolUsuario Rol { get; set; }

    }
}
