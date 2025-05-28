using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Usuarios
{
    public class DetailsModel : PageModel
    {
        private readonly UsuariosService _usuarioService;
        private readonly RolService _rolService;

        public DetailsModel(UsuariosService usuarioService, RolService rolService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
        }

        public Usuario Usuario { get; set; }
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Usuario = await _usuarioService.GetUsuarioById(id);
            // Cargar Roles
            var roles = await _rolService.GetRolesAsync();
            Roles = roles.Select(r => new SelectListItem
            {
                Value = r.IdRol.ToString(),
                Text = r.NombreRol
            }).ToList();

            if (Usuario == null)
            {
                return NotFound();
            }
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
