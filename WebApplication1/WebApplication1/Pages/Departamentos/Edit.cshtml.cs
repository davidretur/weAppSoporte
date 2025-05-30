using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Departamentos
{
    public class EditModel : PageModel
    {
        private readonly DepartamentoService _departamentoService;

        public EditModel(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        [BindProperty]
        public Models.Departamento Departamento { get; set; } = new Models.Departamento();
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
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error fetching department: {ex.Message}");
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (!ModelState.IsValid)
            {
                return Page();
            }
 
                await _departamentoService.UpdateDepartamentoAsync(id, Departamento);
                return RedirectToPage("./Index");
            }

            catch (HttpRequestException ex) when (ex.Message.Contains("400") || ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("Departamento.NombreDepartamento", "Ya existe un departamento con ese nombre o no cambiaste nada.");
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado. Intenta nuevamente.");
                return Page();
            }
        }
    }
}