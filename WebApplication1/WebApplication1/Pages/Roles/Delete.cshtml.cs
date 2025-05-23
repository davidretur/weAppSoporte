using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly RolService _rolService;
        public DeleteModel(RolService rolService)
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
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _rolService.DeleteRolesAsync(id);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (HttpRequestException ex)
            {
                // Log the error
                Console.WriteLine($"Error al dar de Baja Rol: {ex.Message}");
                ModelState.AddModelError(string.Empty, "El error ocurrio al intentar dar de baja el Rol.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
