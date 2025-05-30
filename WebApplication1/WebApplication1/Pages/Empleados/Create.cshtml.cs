using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Empleados
{
    public class CreateModel : PageModel
    {
        private readonly EmpleadoService _empleadoService;
        private readonly DepartamentoService _departamentoService;
        private readonly UbicacionService _ubicacionService;

        public CreateModel(
            EmpleadoService empleadoService,
            DepartamentoService departamentoService,
            UbicacionService ubicacionService)
        {
            _empleadoService = empleadoService;
            _departamentoService = departamentoService;
            _ubicacionService = ubicacionService;
        }

        [BindProperty]
        public Empleado Empleado { get; set; } = new Empleado();

        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Ubicaciones { get; set; }

        private async Task CargarCombosAsync()
        {
            var departamentos = await _departamentoService.GetAllDepartamentos();

            Departamentos = departamentos.Select(d => new SelectListItem
            {
                Value = d.IdDepartamento.ToString(),
                Text = d.NombreDepartamento
            }).ToList();

            var ubicaciones = await _ubicacionService.GetAllUbicaciones();
            Ubicaciones = ubicaciones.Select(u => new SelectListItem
            {
                Value = u.IdUbicacion.ToString(),
                Text = u.Zona
            }).ToList();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try {
                await CargarCombosAsync();
                return Page();
            }
            catch (UnauthorizedAccessException)
            {
                // Redirige a la página de login
                return RedirectToPage("/Sesion/Login");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Recargar listas si hay error de validación
                    await OnGetAsync();
                    return Page();
                }
                await _empleadoService.CreateEmpleadoAsync(Empleado);
                return RedirectToPage("./Index");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("400") || ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("Empleado.Correo", "Ya existe un empleado con estos datos.");
                ModelState.AddModelError("Empleado.Nombre", "Ya existe un empleado con estos datos.");
                ModelState.AddModelError("Empleado.IdDepartamento", "Ya existe un empleado con estos datos.");
                ModelState.AddModelError("Empleado.IdUbicacion", "Ya existe un empleado con estos datos.");
                await CargarCombosAsync();
                return Page();
            }
        
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating Empleado: {ex.Message}");
                await CargarCombosAsync();
                return Page();
            }
        }
    }
}