using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;
using WebAppSoporte.Services;
using static WebAppInventarioS.Services.DepartamentoService;

namespace WebAppInventarioS.Pages.Departamentos
{
    public class CreateModel : PageModel
    {
        private readonly DepartamentoService _departamentoService;
        private readonly AuthState _authState;

        public CreateModel(DepartamentoService departamentoService, AuthState authState)
        {
            _departamentoService = departamentoService;
            _authState = authState;
        }
        [BindProperty]
        public Departamento Departamento { get; set; } = new Departamento();
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Verifica si el usuario est� autenticado (tiene token v�lido)
                if (string.IsNullOrEmpty(_authState?.Token) || !_authState.IsAuthenticated)
                {
                    // Redirige al login si no hay token o no est� autenticado
                    return RedirectToPage("/Sesion/Login");
                }
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var result = await _departamentoService.CreateDepartamentoAsync(Departamento);
                if (result != null)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating department. Please try again.");
                    return Page();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurri� un error inesperado. Intenta nuevamente.");
                return Page();
            }
        }
    }
}
