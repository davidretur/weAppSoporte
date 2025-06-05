using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;
using WebAppSoporte.Services;

namespace WebAppSoporte.Pages.LicenciasOffice
{
    public class EditModel : PageModel
    {
        private readonly LicenciaOfficeService _licenciaOfficeService;
        private readonly EquiposService _equiposService;
        public EditModel(LicenciaOfficeService licenciaOfficeService, EquiposService equiposService)
        {
            _licenciaOfficeService = licenciaOfficeService;
            _equiposService = equiposService;
        }
        [BindProperty]
        public LicenciaOffice licenciaOffice { get; set; }
        public List<SelectListItem> Equipos { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            licenciaOffice = await _licenciaOfficeService.GetLicenciaOfficeById(id);
            if (licenciaOffice == null)
            {
                return NotFound();
            }
            // Llenar el combo de equipos
            await LlenarCombo();
            return Page();
        }

        public async Task LlenarCombo()
        {
            // Obtener el equipo por Id
            var equipo = await _equiposService.GetEquipoById(licenciaOffice.IdEquipo);
            if (equipo != null)
            {
                Equipos = new List<SelectListItem>
            {
            new SelectListItem
            {
                Value = equipo.IdEquipo.ToString(),
                Text = equipo.NumeroSerie // O el campo que prefieras mostrar
            }
            };
            }
        }
    }
}
