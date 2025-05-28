using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Departamentos
{
    public class DeleteModel : PageModel
    {
        private readonly DepartamentoService _departamentoService;

        public DeleteModel(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        [BindProperty]
        public Departamento Departamento { get; set; } = new Departamento();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Departamento = await _departamentoService.GetDepartamentoByIdAsync(id);
            if (Departamento == null)
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
            try
            {
                if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _departamentoService.DeleteDepartamentoAsync(id);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (HttpRequestException ex)
            {
                // Log the error
                Console.WriteLine($"Error al dar de Baja Departamento: {ex.Message}");
                ModelState.AddModelError(string.Empty, "El error ocurrio al intentar dar de baja el Départamento.");
                return Page();
            }

            return RedirectToPage("./Index");
            }
            catch (UnauthorizedAccessException)
            {
                // Redirige a la página de login
                return RedirectToPage("/Sesion/Login");
            }
        }
    }
}
