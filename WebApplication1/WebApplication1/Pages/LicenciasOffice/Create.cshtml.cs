using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppSoporte.Services;

namespace WebAppSoporte.Pages.LicenciasOffice
{
    public class CreateModel : PageModel
    {

        private readonly LicenciaOfficeService _licenciaOfficeService;

        public CreateModel(LicenciaOfficeService licenciaOfficeService)
        {
            _licenciaOfficeService = licenciaOfficeService;
        }
        [BindProperty]
        public LicenciaOffice LicenciaOffice { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _licenciaOfficeService.CreateLicenciaOffice(LicenciaOffice);
            return RedirectToPage("./Index");
        }
    }
}
