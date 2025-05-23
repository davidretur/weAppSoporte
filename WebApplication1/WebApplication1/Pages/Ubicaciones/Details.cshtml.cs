using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Ubicaciones
{
    public class DetailsModel : PageModel
    {
        private readonly UbicacionService _ubicacionService;

        public DetailsModel(UbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }
        [BindProperty]
        public Ubicacion Ubicacion { get; set; } = new Ubicacion();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Ubicacion = await _ubicacionService.GetUbicacionById(id);
            if (Ubicacion == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
