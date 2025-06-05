using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppInventarioS.Models;
using WebAppSoporte.Services;

namespace WebAppSoporte.Pages.LicenciasOffice
{
    public class DeleteModel : PageModel
    {
        private readonly LicenciaOfficeService _licenciaOfficeService;

        public DeleteModel(LicenciaOfficeService licenciaOfficeService)
        {
            _licenciaOfficeService = licenciaOfficeService;
        }

        [BindProperty]
        public LicenciaOffice LicenciaOffice { get; set; } = new LicenciaOffice();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            LicenciaOffice = await _licenciaOfficeService.GetLicenciaOfficeById(id);
            if (LicenciaOffice == null)
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
                await _licenciaOfficeService.DeleteLicenciaOffice(id);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar la licencia: {ex.Message}");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
