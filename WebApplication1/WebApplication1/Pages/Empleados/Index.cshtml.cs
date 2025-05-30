using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models.Dtos;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Empleados
{
    public class IndexModel : PageModel
    {
        private readonly EmpleadoService _empleadoService;
        private readonly DepartamentoService _departamentoService;
        private readonly UbicacionService _ubicacionService;

        public IndexModel(EmpleadoService empleadoService, DepartamentoService departamentoService, UbicacionService ubicacionService)
        {
            _empleadoService = empleadoService;
            _departamentoService = departamentoService;
            _ubicacionService = ubicacionService;
        }
        public List<EmpleadoDto> empleados { get; set; } = new List<EmpleadoDto>();

        [BindProperty(SupportsGet = true)]
        public string busqueda { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Solo realiza la búsqueda si hay un término de búsqueda
                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    empleados = await _empleadoService.GetEmpleados(
                    _departamentoService,
                    _ubicacionService,
                    busqueda
                );
                }
                else
                {
                    // Si no hay término de búsqueda, obtiene todos los empleados
                    empleados = [];
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
