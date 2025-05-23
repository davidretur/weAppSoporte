using System.ComponentModel.DataAnnotations;

namespace WebAppInventarioS.Models
{
    public class Rol
    {
        public int IdRol { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreRol { get; set; }
        public bool Status { get; set; }
    }
}
