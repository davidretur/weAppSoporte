using WebAppInventarioS.Models;
using WebAppSoporte.Services;

namespace WebAppInventarioS.Services
{
    public class UbicacionService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;
        public UbicacionService(IHttpClientFactory httpClientFactory, AuthState authState)
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
        public async Task<List<Ubicacion>> GetAllUbicaciones()
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync("api/Ubicacion");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Ubicacion>>();
        }
        public async Task<Ubicacion> GetUbicacionById(int? id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Ubicacion/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Ubicacion>();
        }
        public async Task<Ubicacion> CreateUbicacion(Ubicacion ubicacion)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("api/Ubicacion", ubicacion);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Ubicacion>();
        }
        public async Task UpdateUbicacion(int id, Ubicacion ubicacion)
        {
            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/Ubicacion/{id}", ubicacion);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteUbicacionAsync(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"api/Ubicacion/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
