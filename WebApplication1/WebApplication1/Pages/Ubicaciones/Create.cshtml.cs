using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Ubicaciones
{
    public class CreateModel : PageModel
    {
        private readonly UbicacionService _ubicacionService;

        public CreateModel(UbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }
        [BindProperty]
        public Ubicacion Ubicacion { get; set; } = new Ubicacion();
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _ubicacionService.CreateUbicacion(Ubicacion);
            return RedirectToPage("./Index");
        }
    }
}