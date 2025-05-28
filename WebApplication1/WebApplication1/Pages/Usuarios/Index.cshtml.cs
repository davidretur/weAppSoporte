using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;
using WebAppSoporte.Models.Dtos;

namespace WebAppInventarioS.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly UsuariosService _usuariosService;
        private readonly RolService _rolService;
        public IndexModel(UsuariosService usuariosService, RolService rolService)
        {
            _usuariosService = usuariosService;
            _rolService = rolService;
        }
        public List<UsuarioDto> Usuarios { get; set; } = new List<UsuarioDto>();
        public List<Rol> Roles { get; set; } = new List<Rol>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Obtener todos los roles
                Roles = await _rolService.GetRolesAsync();

            // Obtener todos los usuarios
            var usuarios = await _usuariosService.GetAllUsuarios();

            // Mapear usuarios a UsuarioDto y asignar el NombreRol correspondiente
            Usuarios = usuarios.Select(u =>
            {
                var rol = Roles.FirstOrDefault(r => r.IdRol == u.IdRol);
                return new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreCompleto = u.NombreCompleto,
                    Correo = u.Correo,
                    Puesto = u.Puesto,
                    UsuarioSesion = u.UsuarioSesion,
                    Contracena = u.Contracena,
                    NombreRol = rol?.NombreRol ?? string.Empty,
                    Status = u.Status
                };
            }).ToList();
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
