using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly UsuariosService _usuariosService;
        public IndexModel(UsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public async Task OnGetAsync()
        {
            Usuarios = await _usuariosService.GetAllUsuarios();
        }
    }
}
