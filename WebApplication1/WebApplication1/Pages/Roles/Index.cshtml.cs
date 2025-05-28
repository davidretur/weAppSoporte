using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly RolService _rolService;
        public IndexModel(RolService rolService)
        {
            _rolService = rolService;
        }
        public List<Rol> Roles { get; set; } = new List<Rol>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Roles = await _rolService.GetRolesAsync();
                return Page();
            }
            catch (UnauthorizedAccessException)
            {
                // Redirige a la página de login
                return RedirectToPage("/Sesion/Login");
            }
        }
    }
}
