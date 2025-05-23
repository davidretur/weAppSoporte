using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly RolService _rolService;
        public EditModel(RolService rolService)
        {
            _rolService = rolService;
        }
        [BindProperty]
        public Models.Rol Rol { get; set; } = new Models.Rol();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Rol = await _rolService.GetRolesAsync(id);
            if (Rol == null)
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
                await _rolService.UpdateRolesAsync(id, Rol);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating role: {ex.Message}");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
