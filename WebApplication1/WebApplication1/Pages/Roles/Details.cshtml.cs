using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly RolService _rolService;
        public DetailsModel(RolService rolService)
        {
            _rolService = rolService;
        }
        [BindProperty]
        public Rol Rol { get; set; } = new Rol();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Rol = await _rolService.GetRolesAsync(id);
            if (Rol == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
