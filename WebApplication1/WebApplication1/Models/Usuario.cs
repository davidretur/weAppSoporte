using System.ComponentModel.DataAnnotations;

namespace WebAppInventarioS.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(100)]
        public string Puesto { get; set; }

        [StringLength(50)]
        public string UsuarioSesion { get; set; }

        [StringLength(300)]
        public string Contracena { get; set; }

        public int IdRol { get; set; }
        public bool Status { get; set; }
    }
}
