using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Ubicaciones
{
    public class EditModel : PageModel
    {
        private readonly UbicacionService _ubicacionService;
        public EditModel(UbicacionService ubicacionService)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _ubicacionService.UpdateUbicacion(id, Ubicacion);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al modificar Ubicacion: {ex.Message}");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
