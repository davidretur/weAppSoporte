using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Empleados
{
    public class EditModel : PageModel
    {
        private readonly EmpleadoService _empleadoService;
        private readonly DepartamentoService _departamentoService;
        private readonly UbicacionService _ubicacionService;

        public EditModel(EmpleadoService empleadoService, DepartamentoService departamentoService, UbicacionService ubicacionService)
        {
            _empleadoService = empleadoService;
            _departamentoService = departamentoService;
            _ubicacionService = ubicacionService;
        }
        [BindProperty]
        public Empleado Empleado { get; set; } = new Empleado();

        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Ubicaciones { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar empleado
            Empleado = await _empleadoService.GetEmpleadoByIdAsync(id.Value);
            if (Empleado == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _empleadoService.UpdateEmpleadoAsync(id, Empleado);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating Empleado: {ex.Message}");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
