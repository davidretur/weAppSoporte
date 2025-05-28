using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Ubicaciones
{
    public class IndexModel : PageModel
    {
        private readonly UbicacionService _ubicacionService;
        public IndexModel(UbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }
        public List<Ubicacion> Ubicaciones { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Ubicaciones = await _ubicacionService.GetAllUbicaciones();
                return Page();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToPage("/Sesion/Login");
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
