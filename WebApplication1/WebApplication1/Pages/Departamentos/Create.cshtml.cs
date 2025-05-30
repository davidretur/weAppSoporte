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
                // Verifica si el usuario está autenticado (tiene token válido)
                if (string.IsNullOrEmpty(_authState?.Token) || !_authState.IsAuthenticated)
                {
                    // Redirige al login si no hay token o no está autenticado
                    return RedirectToPage("/Sesion/Login");
                }
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var result = await _departamentoService.CreateDepartamentoAsync(Departamento);
                if (result == null)
                {
                    ModelState.AddModelError("Departamento.NombreDepartamento", "Ya existe un departamento con ese nombre.");
                    return Page();
                }
                return RedirectToPage("./Index");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("400") || ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("Departamento.NombreDepartamento", "Ya existe un departamento con ese nombre.");
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado. Intenta nuevamente.");
                return Page();
            }
        }
    }
}
