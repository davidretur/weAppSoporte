using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Departamentos
{
    public class IndexModel : PageModel
    {
        private readonly DepartamentoService _departamentoService;
        public IndexModel(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        public List<Departamento> departamentos { get; set; } = new List<Departamento>();

        public async Task OnGetAsync()
        {
            departamentos = await _departamentoService.GetAllDepartamentos();
        }
    }
}
