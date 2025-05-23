using System.ComponentModel.DataAnnotations;

namespace WebAppInventarioS.Models
{
    public class LicenciaOffice
    {
        public int IdLicencia { get; set; }

        [Required]
        [StringLength(100)]
        public string Cuenta { get; set; }

        [Required]
        public int IdEquipo { get; set; }
        [Required]
        public bool Status { get; set; } = true;
    }
}
