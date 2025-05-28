using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppSoporte.Services;

namespace WebAppSoporte.Pages.Sesion
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;
        public LoginModel(AuthService authService)
        {
            _authService = authService;
        }
        [BindProperty]
        public string UsuarioSesion { get; set; }
        [BindProperty]
        public string Contrasena { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var isAuthenticated = await _authService.AuthenticateUserAsync(UsuarioSesion, Contrasena);

            if (isAuthenticated)
            {
                // Redirige a la página principal o protegida
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Usuario o contraseña incorrectos.";
                return Page();
            }
        }

    }
}
