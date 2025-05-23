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
        private readonly EmpleadoService _empleadoService;
        public EditModel(UsuariosService usuarioService, RolService rolService, EmpleadoService empleadoService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
            _empleadoService = empleadoService; 
        }
        [BindProperty]
        public Usuario Usuario { get; set; } = new Usuario();
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Empleados { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Usuario = await _usuarioService.GetUsuarioById(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            // Cargar Roles
            var departamentos = await _rolService.GetRolesAsync();
            Roles = departamentos.Select(d => new SelectListItem
            {
                Value = d.IdRol.ToString(),
                Text = d.NombreRol
            }).ToList();
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
                await _usuarioService.UpdateUsuario(id, Usuario);  
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al modificar Ubicacion: {ex.Message}");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
