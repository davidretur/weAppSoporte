using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Models.Dtos;
using WebAppInventarioS.Services;
using WebAppSoporte.Models.Dtos;
using WebAppSoporte.Services;

namespace WebAppInventarioS.Pages.Equipos
{
    public class IndexModel : PageModel
    {
        private readonly EquiposService _equiposService;
        private readonly UbicacionService _ubicacionService;
        private readonly DepartamentoService _departamentoService;
        private readonly EmpleadoService _empleadoService;
        private readonly LicenciaOfficeService _licenciaService;  

        public IndexModel(EquiposService equiposService, UbicacionService ubicacionService, DepartamentoService departamentoService, EmpleadoService empleadoService, LicenciaOfficeService licenciaOfficeService)
        {
            _equiposService = equiposService;
            _ubicacionService = ubicacionService;
            _departamentoService = departamentoService;
            _empleadoService = empleadoService;
            _licenciaService = licenciaOfficeService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public List<EquipoDto> Equipos { get; set; } = new List<EquipoDto>();
        public List<Empleado> Empleados { get; set; } = new List<Empleado>();
        public List<LicenciaOfEqDto> Licencias { get; set; } = new List<LicenciaOfEqDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                Equipos = new List<EquipoDto>();
            }
            else { }
            var equipos = await _equiposService.GetAllEquiposLink(
                _departamentoService,
                _ubicacionService,
                SearchTerm);

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                Equipos = equipos.Where(e =>
                    (!string.IsNullOrEmpty(e.NumeroSerie) && e.NumeroSerie.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(e.Etiqueta) && e.Etiqueta.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(e.Modelo) && e.Modelo.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }
            else
            {
                Equipos = equipos;
            }

            Empleados = new List<Empleado>();
            foreach (var equipo in Equipos)
            {
                var empleado = await _empleadoService.GetEmpleadoByIdAsync(equipo.IdEmpleado);
                if (empleado != null)
                    Empleados.Add(empleado);
            }
            Licencias = new List<LicenciaOfEqDto>();
            foreach (var equipo in Equipos)
            {
                var licencia = await _licenciaService.GetLicenciaOfficeByEquipod(equipo.IdEquipo);
                if (licencia != null)
                    Licencias.Add(licencia);
            }
            return Page();
        }
            catch (UnauthorizedAccessException)
            {
                // Redirige a la página de login
                return RedirectToPage("/Sesion/Login");
    }
}
    }
}
