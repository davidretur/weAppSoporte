using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Threading.Tasks;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppSoporte.Pages.Usuarios
{
    public class ContracenaModel : PageModel
    {
        private readonly UsuariosService _usuarioService;

        public ContracenaModel(UsuariosService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [BindProperty]
        public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                // Lógica para obtener el usuario de la BD
                Usuario = await _usuarioService.GetUsuarioById(id);

            // Si no tiene contraseña, genera una nueva
            if (string.IsNullOrEmpty(Usuario.Contracena))
            {
                Usuario.Contracena = ""; // Inicializa como cadena vacía
            }
            Usuario.Contracena = GenerarContrasena(8);
                return Page();
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al obtener el usuario: {ex.Message}");
                return Page();
            }
            catch (UnauthorizedAccessException)
            {
                // Redirige a la página de login
                return RedirectToPage("/Sesion/Login");
            }
        }

        private string GenerarContrasena(int longitud)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(caracteres, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                await _usuarioService.UpdateContracena(Usuario.IdUsuario, Usuario.Contracena);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al modificar Contraceña: {ex.Message}");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
