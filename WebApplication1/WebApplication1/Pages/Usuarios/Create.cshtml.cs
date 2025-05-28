using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly UsuariosService _usuariosService;
        private readonly RolService _rolService;
        private readonly EmpleadoService _empleadoService;


        public CreateModel(UsuariosService usuariosService, RolService rolService, EmpleadoService empleadoService)
        {
            _usuariosService = usuariosService;
            _rolService = rolService;
            _empleadoService = empleadoService;
        }
        [BindProperty]
        public Usuario Usuario { get; set; } = new Usuario();

        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Empleados { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
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
        public async Task<IActionResult> OnPostAsync()
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
                return Page();
            }

            var usuariosExistentes = await _usuariosService.GetAllUsuarios();
            if (usuariosExistentes.Any(u => u.Correo.Equals(Usuario.Correo, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Usuario.Correo", "Ya existe un usuario con este correo.");
                return Page();
            }

            if (usuariosExistentes.Any(u => u.UsuarioSesion.Equals(Usuario.UsuarioSesion, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Usuario.UsuarioSesion", "Ya existe un usuario con este nombre de usuario.");
                return Page();
            }

            await _usuariosService.CreateUsuario(Usuario);
            return RedirectToPage("./Index");
        }
    }
}
