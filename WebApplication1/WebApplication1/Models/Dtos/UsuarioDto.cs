using System.ComponentModel.DataAnnotations;

namespace WebAppSoporte.Models.Dtos
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Puesto { get; set; }
        public string UsuarioSesion { get; set; }
        public string Contracena { get; set; }
        public string NombreRol { get; set; }
        public bool Status { get; set; }
    }
}
