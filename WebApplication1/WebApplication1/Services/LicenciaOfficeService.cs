using WebAppInventarioS.Models;
using WebAppSoporte.Models.Dtos;

namespace WebAppSoporte.Services
{
    public class LicenciaOfficeService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;
        public LicenciaOfficeService(IHttpClientFactory httpClientFactory, AuthState authState)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
            _authState = authState;
        }
        private void AddJwtHeader()
        {
            if (!string.IsNullOrEmpty(_authState.Token) && !_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authState.Token);
            }
        }
        public async Task<List<LicenciaOffice>> GetAllLicenciasOffice()
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync("api/LicenciaOff");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LicenciaOffice>>();
        }
        public async Task<LicenciaOffice> GetLicenciaOfficeById(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/LicenciaOff/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LicenciaOffice>();
        }
        public async Task<LicenciaOfEqDto> GetLicenciaOfficeByEquipod(int idEquipo)
        {
            AddJwtHeader();
            return await _httpClient.GetFromJsonAsync<LicenciaOfEqDto>($"api/LicenciaOff/Equipo/{idEquipo}");
        }
        public async Task<LicenciaOffice> CreateLicenciaOffice(LicenciaOffice licenciaOffice)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("api/LicenciaOff", licenciaOffice);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LicenciaOffice>();
            }
            return null;
        }
        public async Task<bool> UpdateLicenciaOffice(LicenciaOffice licenciaOffice)
        {
            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/LicenciaOff/{licenciaOffice.IdLicencia}", licenciaOffice);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteLicenciaOffice(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"api/LicenciaOff/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
