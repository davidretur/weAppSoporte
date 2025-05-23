using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Ubicaciones
{
    public class DeleteModel : PageModel
    {
        private readonly UbicacionService _ubicacionService;
        public DeleteModel(UbicacionService ubicacionService)
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
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _ubicacionService.DeleteUbicacionAsync(id);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Error al dar de Baja Ubicacion: {ex.Message}");
                ModelState.AddModelError(string.Empty, "El error ocurrio al intentar dar de baja la Ubicacion.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
