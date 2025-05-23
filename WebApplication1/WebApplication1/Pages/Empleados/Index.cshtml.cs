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

        public async Task OnGetAsync()
        {
            empleados = await _empleadoService.GetAllEmpleados(
                _departamentoService,
                _ubicacionService
            );
        }
    }
}
