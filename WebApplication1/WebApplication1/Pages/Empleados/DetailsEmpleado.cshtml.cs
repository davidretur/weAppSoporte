using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Empleados
{
    public class DetailsEmpleadoModel : PageModel
    {
        private readonly EmpleadoService _empleadoService;
        private readonly DepartamentoService _departamentoService;
        private readonly UbicacionService _ubicacionService;
        private readonly EquiposService _equiposService;

        public DetailsEmpleadoModel(EmpleadoService empleadoService, DepartamentoService departamentoService,
            UbicacionService ubicacionService, EquiposService equiposService)
        {
            _empleadoService = empleadoService;
            _departamentoService = departamentoService;
            _ubicacionService = ubicacionService;
            _equiposService = equiposService;
        }
        [BindProperty]
        public Empleado Empleado { get; set; } = new Empleado();
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Ubicaciones { get; set; }
        public List<Equipo> Equipos { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try { 
            if (id == null)
            {
                return NotFound();
            }

            // Cargar empleado
            Empleado = await _empleadoService.GetEmpleadoByIdAsync(id);
            if (Empleado == null)
            {
                return NotFound();
            }
                // Cargar equipo asociado al empleado
                Equipos = await _equiposService.GetEquipoEmpleadoById(id);

                // Cargar departamentos
                var departamentos = await _departamentoService.GetAllDepartamentos();
            Departamentos = departamentos.Select(d => new SelectListItem
            {
                Value = d.IdDepartamento.ToString(),
                Text = d.NombreDepartamento
            }).ToList();

            // Cargar ubicaciones
            var ubicaciones = await _ubicacionService.GetAllUbicaciones();
            Ubicaciones = ubicaciones.Select(u => new SelectListItem
            {
                Value = u.IdUbicacion.ToString(),
                Text = u.Zona
            }).ToList();

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
