using System.ComponentModel.DataAnnotations;

namespace WebAppInventarioS.Models
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreDepartamento { get; set; }

        public bool Status { get; set; } = true;
    }
}
