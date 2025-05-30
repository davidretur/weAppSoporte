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
        public List<EmpleadoDto> Empleados { get; set; } = new List<EmpleadoDto>();
        public List<LicenciaOfEqDto> Licencias { get; set; } = new List<LicenciaOfEqDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Equipos = new List<EquipoDto>();
                Empleados = new List<EmpleadoDto>();
                Licencias = new List<LicenciaOfEqDto>();

                // Solo realiza la búsqueda si hay un término de búsqueda
                if (!string.IsNullOrWhiteSpace(SearchTerm))
                {
                    var equipos = await _equiposService.GetAllEquiposLink(
                        _departamentoService,
                        _ubicacionService,
                        SearchTerm);

                    Equipos = equipos.Where(e =>
                        (!string.IsNullOrEmpty(e.NumeroSerie) && e.NumeroSerie.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(e.Etiqueta) && e.Etiqueta.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(e.Modelo) && e.Modelo.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    ).ToList();

                    foreach (var equipo in Equipos)
                    {
                        var empleado = await _empleadoService.GetEmpleadoByIdEquipo(equipo.IdEquipo,
                            _departamentoService,
                            _ubicacionService);
                        if (empleado != null)
                            Empleados.Add(empleado);
                    }

                    foreach (var equipo in Equipos)
                    {
                        var licencia = await _licenciaService.GetLicenciaOfficeByEquipod(equipo.IdEquipo);
                        if (licencia != null)
                            Licencias.Add(licencia);
                    }
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
