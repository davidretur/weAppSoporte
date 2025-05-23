using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Departamentos
{
    public class DetailsModel : PageModel
    {
        private readonly DepartamentoService _departamentoService;
        public DetailsModel(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        [BindProperty]
        public Departamento departamento { get; set; } = new Models.Departamento();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            departamento = await _departamentoService.GetDepartamentoByIdAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
