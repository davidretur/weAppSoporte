using System.ComponentModel.DataAnnotations;

namespace WebAppSoporte.Models.Dtos
{
    public class LicenciaOfficeDto
    {
        public int IdLicencia { get; set; }

        [Required]
        [StringLength(100)]
        public string Cuenta { get; set; }

        [Required]
        public string NumeroSerie { get; set; }
        [Required]
        public bool Status { get; set; } = true;
    }
}
