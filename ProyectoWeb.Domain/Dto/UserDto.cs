using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Domain.Dto
{
    public class UserDto
    {
        public int IdUser { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Nombre del Usuario es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria")]
        public DateTime Fecha_Nacimiento { get; set; }
        public string StrFecha_Nacimiento { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "El sexo es obligatorio")]
        public string Sexo { get; set; }
        public string SexoGeneral { get; set; }
       

    }
}
