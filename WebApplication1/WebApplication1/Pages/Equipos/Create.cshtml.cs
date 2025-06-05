using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Equipos
{
    public class CreateModel : PageModel
    {
        private readonly EquiposService _equiposService;
        private readonly UbicacionService  _ubicacionService;
        private readonly DepartamentoService _departamentoService;
        private readonly EmpleadoService _empleadoService;
        public CreateModel(EquiposService equiposService, UbicacionService ubicacionService, DepartamentoService departamentoService, EmpleadoService empleadoService)
        {
            _equiposService = equiposService;
            _ubicacionService = ubicacionService;
            _departamentoService = departamentoService;
            _empleadoService = empleadoService;
        }
        [BindProperty]
        public Equipo Equipo { get; set; } = new Equipo();
        public List<SelectListItem> Ubicaciones { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Departamentos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Empleados { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            Ubicaciones = (await _ubicacionService.GetAllUbicaciones())
                .Select(u => new SelectListItem { Value = u.IdUbicacion.ToString(), Text = u.Zona }).ToList();

            Departamentos = (await _departamentoService.GetAllDepartamentos())
                .Select(d => new SelectListItem { Value = d.IdDepartamento.ToString(), Text = d.NombreDepartamento }).ToList();
            // Agrega la opción "Sin Asignar" al inicio
            Departamentos.Insert(0, new SelectListItem { Value = "0", Text = "Sin Asignar" });

            Empleados = (await _empleadoService.GetAllEmpleadosU())
                .Select(e => new SelectListItem { Value = e.IdEmpleado.ToString(), Text = e.Nombre + " " + e.ApellidoP + " " + e.ApellidoM }).ToList();
            // Agrega la opción "Sin Asignar" al inicio
            Empleados.Insert(0, new SelectListItem { Value = "0", Text = "Sin Asignar" });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar listas si hay error de validación
                await OnGetAsync();
                return Page();
            }
            await _equiposService.CreateEquipo(Equipo);
            return RedirectToPage("./Index");
        }
    }
}
