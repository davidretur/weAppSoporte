using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;
using WebAppSoporte.Models.Dtos;

namespace WebAppInventarioS.Pages.Usuarios
{
    public class DeleteModel : PageModel
    {
        private readonly UsuariosService _usuarioService;
        private readonly RolService _rolService;    
        public DeleteModel(UsuariosService usuarioService, RolService rolService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
        }
        [BindProperty]


        public UsuarioDto Usuarios { get; set; } = new UsuarioDto();
        public List<Rol> Roles { get; set; } = new List<Rol>();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                // Obtener el usuario por id
                var usuario = await _usuarioService.GetUsuarioById(id);

            // Obtener el rol del usuario
            var rol = await _rolService.GetRolesAsync(usuario.IdRol);

            // Asignar el rol al objeto Roles (en lugar de una lista)
            Roles = new List<Rol> { rol };

            // Mapear el usuario a UsuarioDto y asignar el NombreRol correspondiente
            Usuarios = new UsuarioDto
    {

            IdUsuario = usuario.IdUsuario,
            NombreCompleto = usuario.NombreCompleto,
            Correo = usuario.Correo,
            Puesto = usuario.Puesto,
            UsuarioSesion = usuario.UsuarioSesion,
            Contracena = usuario.Contracena,
            NombreRol = rol?.NombreRol ?? string.Empty,
            Status = usuario.Status
        
    };
                    if (Usuarios == null)
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
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            try
            {
                await _usuarioService.DeleteUsuarioAsync(id);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Error al dar de Baja Ubicacion: {ex.Message}");
                ModelState.AddModelError(string.Empty, "El error ocurrio al intentar dar de baja la Ubicacion.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
