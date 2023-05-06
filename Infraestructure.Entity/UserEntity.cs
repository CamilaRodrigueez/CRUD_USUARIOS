using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Entity
{
    public class UserEntity
    {
        public int IdUser { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Nombre del Usuario es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria")]
        public DateTime Fecha_Nacimiento { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "El sexo es obligatorio")]
        public string Sexo { get; set; }
    }
}