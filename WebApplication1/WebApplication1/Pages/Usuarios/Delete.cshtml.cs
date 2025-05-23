using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

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
        public Usuario Usuario { get; set; } = new Usuario();
        public List<SelectListItem> Roles { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Usuario = await _usuarioService.GetUsuarioById(id);
            // Cargar Roles
            var departamentos = await _rolService.GetRolesAsync();
            Roles = departamentos.Select(d => new SelectListItem
            {
                Value = d.IdRol.ToString(),
                Text = d.NombreRol
            }).ToList();
            if (Usuario == null)
            {
                return NotFound();
            }
            return Page();
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
