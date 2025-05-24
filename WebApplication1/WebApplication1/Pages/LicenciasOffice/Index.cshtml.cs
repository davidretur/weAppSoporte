using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppSoporte.Services;

namespace WebAppSoporte.Pages.LicenciasOffice
{
    public class IndexModel : PageModel
    {
        private readonly LicenciaOfficeService _licenciaOfficeService; 

        public IndexModel(LicenciaOfficeService licenciaOfficeService)
        {
            _licenciaOfficeService = licenciaOfficeService;
        }
        public List<LicenciaOffice> LicenciasOffice { get; set; } = new List<LicenciaOffice>();
        public async Task OnGet()
        {
            LicenciasOffice = await _licenciaOfficeService.GetAllLicenciasOffice();
        }

    }
}
