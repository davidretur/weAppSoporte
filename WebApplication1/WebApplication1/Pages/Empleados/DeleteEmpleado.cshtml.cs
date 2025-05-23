using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public Models.Empleado Empleado { get; set; } = new Models.Empleado();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Empleado = await _empleadoService.GetEmpleadoByIdAsync(id);
            if (Empleado == null)
            {
                return NotFound();
            }
            return Page();
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

            return RedirectToPage("./Index");
        }
    }
}
