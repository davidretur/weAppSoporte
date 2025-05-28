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
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                empleados = await _empleadoService.GetAllEmpleados(
                    _departamentoService,
                    _ubicacionService
                );

                if (!string.IsNullOrWhiteSpace(SearchTerm))
                {
                    var term = SearchTerm.Trim().ToLower();
                    empleados = empleados.Where(e =>
                        ($"{e.Nombre} {e.ApellidoP} {e.ApellidoM}".ToLower().Contains(term) ||
                         (e.UsuarioWindows?.ToLower().Contains(term) ?? false) ||
                         (e.Correo?.ToLower().Contains(term) ?? false)
                        )
                    ).ToList();
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
