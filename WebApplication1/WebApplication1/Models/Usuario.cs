using System.ComponentModel.DataAnnotations;

namespace WebAppInventarioS.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(50)]
        public string UsuarioSesion { get; set; }
        [Required]
        [StringLength(300)]
        public string Contracena { get; set; }

        [Required]
        public int IdRol { get; set; }
        public bool Status { get; set; }
    }
}
