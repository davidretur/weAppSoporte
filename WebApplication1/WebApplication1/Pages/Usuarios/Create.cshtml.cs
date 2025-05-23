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
            var roles = await _rolService.GetRolesAsync();
            Roles = roles.Select(r => new SelectListItem
            {
                Value = r.IdRol.ToString(),
                Text = r.NombreRol
            }).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _usuariosService.CreateUsuario(Usuario);
            return RedirectToPage("./Index");
        }
    }
}
