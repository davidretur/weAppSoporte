using System.ComponentModel.DataAnnotations;

namespace WebAppInventarioS.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string? ApellidoP { get; set; }
        [Required]
        [StringLength(100)]
        public string ApellidoM { get; set; }
        [Required]
        [StringLength(100)]
        public string Puesto { get; set; }
        [Required]
        [StringLength(50)]
        public string UsuarioWindows { get; set; }
        [Required]
        [StringLength(100)]
        public string UsuarioAD { get; set; }
        [Required]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(300)]
        public string Pass { get; set; }
        [Required]
        [StringLength(50)]
        public string Acceso { get; set; }

        [Required]
        public int IdDepartamento { get; set; }

        [Required]
        public int IdUbicacion { get; set; }

        [Required]
        public int IdEquipo { get; set; }
        [Required]
        public int IdLicencia { get; set; }
        public bool Status { get; set; } = true;

    }
}
