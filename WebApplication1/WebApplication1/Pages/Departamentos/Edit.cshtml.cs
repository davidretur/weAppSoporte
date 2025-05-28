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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _departamentoService.UpdateDepartamentoAsync(id, Departamento);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating role: {ex.Message}");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}