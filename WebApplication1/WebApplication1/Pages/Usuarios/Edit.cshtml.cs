using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly UsuariosService _usuarioService;
        private readonly RolService _rolService;

        public EditModel(UsuariosService usuarioService, RolService rolService, EmpleadoService empleadoService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;

        }
        [BindProperty]
        public Usuario Usuario { get; set; } = new Usuario();
        public List<SelectListItem> Roles { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Usuario = await _usuarioService.GetUsuarioById(id);
            if (Usuario == null)
            {
                return NotFound();
            }
            var roles = await _rolService.GetRolesAsync();
            Roles = roles.Select(r => new SelectListItem
            {
                Value = r.IdRol.ToString(),
                Text = r.NombreRol
            }).ToList();
            return Page();
            }
            catch (UnauthorizedAccessException)
            {
                // Redirige a la página de login
                return RedirectToPage("/Sesion/Login");
            }
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Recargar roles para el formulario en caso de error
            var roles = await _rolService.GetRolesAsync();
            Roles = roles.Select(r => new SelectListItem
            {
                Value = r.IdRol.ToString(),
                Text = r.NombreRol
            }).ToList();

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, regresa la página con los errores y los roles cargados
                return Page();
            }

            try
            {
                await _usuarioService.UpdateUsuario(id, Usuario);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el usuario: {ex.Message}");
                return Page();
            }
        }
    }
}
