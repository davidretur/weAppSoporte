using System.ComponentModel.DataAnnotations;

namespace WebAppInventarioS.Models.Dtos
{
    public class EquipoDto
    {
        public int IdEquipo { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroSerie { get; set; }
        [Required]
        [StringLength(50)]
        public string Etiqueta { get; set; }
        [Required]
        [StringLength(50)]
        public string Marca { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        [Required]
        [StringLength(15)]
        public string Ip { get; set; }
        [Required]
        [StringLength(20)]
        public string Ram { get; set; }
        [Required]
        [StringLength(20)]
        public string DiscoDuro { get; set; }
        [Required]
        [StringLength(50)]
        public string Procesador { get; set; }
        [Required]
        [StringLength(50)]
        public string So { get; set; }
        [Required]
        [StringLength(20)]
        public string EquipoEstatus { get; set; }
        [Required]
        [StringLength(50)]
        public string Empresa { get; set; }

        public bool Renovar { get; set; }

        public DateTime? FechaUltimaCaptura { get; set; }

        public DateTime? FechaUltimoMantto { get; set; }

        [Required]
        [StringLength(100)]
        public string ElaboroResponsiva { get; set; }

        [Required]
        public string Zona { get; set; }

        [Required]
        public string NombreDepartamento { get; set; }
        [Required]

        public int IdEmpleado { get; set; }

        public bool Status { get; set; }
    }
}
