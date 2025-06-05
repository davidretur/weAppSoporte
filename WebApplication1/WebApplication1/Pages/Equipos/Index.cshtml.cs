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
        public EquipoDto? Equipo { get; set; }
        public List<EmpleadoDto> Empleados { get; set; } = new List<EmpleadoDto>();
        public List<LicenciaOfEqDto> Licencias { get; set; } = new List<LicenciaOfEqDto>();

        public async Task<IActionResult> OnGetAsync()
{
    try
    {
        Empleados = new List<EmpleadoDto>();
        Licencias = new List<LicenciaOfEqDto>();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            Equipo = await _equiposService.GetAllEquiposLink(
                _departamentoService,
                _ubicacionService,
                SearchTerm);

            if (Equipo.IdEmpleado != 0)
            {
                var empleado = await _empleadoService.GetEmpleadoByIdEquipo(Equipo.IdEmpleado,
                    _departamentoService,
                    _ubicacionService);
                if (empleado != null)
                    Empleados.Add(empleado);

                var licencia = await _licenciaService.GetLicenciaOfficeByEquipod(Equipo.IdEquipo);
                if (licencia != null)
                    Licencias.Add(licencia);
            }
        }

        return Page();
    }
    catch (UnauthorizedAccessException)
    {
        return RedirectToPage("/Sesion/Login");
    }
}

    }
}
