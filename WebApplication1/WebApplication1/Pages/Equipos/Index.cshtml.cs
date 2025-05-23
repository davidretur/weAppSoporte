using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Models.Dtos;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Equipos
{
    public class IndexModel : PageModel
    {
        private readonly EquiposService _equiposService;
        private readonly UbicacionService _ubicacionService;
        private readonly DepartamentoService _departamentoService;
        private readonly EmpleadoService _empleadoService;

        public IndexModel(EquiposService equiposService, UbicacionService ubicacionService, DepartamentoService departamentoService, EmpleadoService empleadoService)
        {
            _equiposService = equiposService;
            _ubicacionService = ubicacionService;
            _departamentoService = departamentoService;
            _empleadoService = empleadoService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public List<EquipoDto> Equipos { get; set; } = new List<EquipoDto>();
        public List<Empleado> Empleados { get; set; } = new List<Empleado>();

        public async Task OnGetAsync()
        {
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
        }

    }
}