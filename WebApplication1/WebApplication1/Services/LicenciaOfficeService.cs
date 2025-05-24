using WebAppInventarioS.Models;
using WebAppSoporte.Models.Dtos;

namespace WebAppSoporte.Services
{
    public class LicenciaOfficeService
    {
        private readonly HttpClient _httpClient;
        public LicenciaOfficeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
        }
        public async Task<List<LicenciaOffice>> GetAllLicenciasOffice()
        {
            return await _httpClient.GetFromJsonAsync<List<LicenciaOffice>>("api/LicenciaOff");
        }
        public async Task<LicenciaOffice> GetLicenciaOfficeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<LicenciaOffice>($"api/LicenciaOff/{id}");
        }
        public async Task<LicenciaOfEqDto> GetLicenciaOfficeByEquipod(int idEquipo)
        {
            return await _httpClient.GetFromJsonAsync<LicenciaOfEqDto>($"api/LicenciaOff/Equipo/{idEquipo}");
        }
        public async Task<LicenciaOffice> CreateLicenciaOffice(LicenciaOffice licenciaOffice)
        {
            var response = await _httpClient.PostAsJsonAsync("api/LicenciaOff", licenciaOffice);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LicenciaOffice>();
            }
            return null;
        }
        public async Task<bool> UpdateLicenciaOffice(LicenciaOffice licenciaOffice)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/LicenciaOff/{licenciaOffice.IdLicencia}", licenciaOffice);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteLicenciaOffice(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/LicenciaOff/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
