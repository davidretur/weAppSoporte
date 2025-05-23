using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Departamentos
{
    public class CreateModel : PageModel
    {
        private readonly DepartamentoService _departamentoService;

        public CreateModel(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        [BindProperty]
        public Departamento Departamento { get; set; } = new Departamento();
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _departamentoService.CreateDepartamentoAsync(Departamento);
            if (result != null)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error creating department. Please try again.");
                return Page();
            }
        }
    }
}
