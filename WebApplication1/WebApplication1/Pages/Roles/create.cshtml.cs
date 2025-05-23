using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Roles
{
    public class createModel : PageModel
    {
        private readonly RolService _rolService;
        public createModel(RolService rolService)
        {
            _rolService = rolService;
        }
        [BindProperty]
        public Rol Rol { get; set; } = new Rol();
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

            await _rolService.CreateRolesAsync(Rol);
            return RedirectToPage("./Index");
        }
    }
}
