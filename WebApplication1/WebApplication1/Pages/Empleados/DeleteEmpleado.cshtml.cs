using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Empleados
{
    public class DeleteEmpleadoModel : PageModel
    {
        private readonly EmpleadoService _empleadoService;

        public DeleteEmpleadoModel(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }
        [BindProperty]
        public Empleado Empleado { get; set; } = new Empleado();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try { 
            Empleado = await _empleadoService.GetEmpleadoByIdAsync(id);
            if (Empleado == null)
            {
                return NotFound();
            }
            return Page();
            }
            catch (UnauthorizedAccessException)
            {
                // Redirige a la página de login
                return RedirectToPage("/Sesion/Login");
            }
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _empleadoService.DeleteEmpleadoAsync(id);
                return RedirectToPage("/Empleados/Index", new { busqueda = Empleado.Nombre + " " + Empleado.ApellidoP + " " + Empleado.ApellidoM });
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (HttpRequestException ex)
            {
                // Log the error
                Console.WriteLine($"Error deleting article: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the article.");
                return Page();
            }
        }
    }
}
