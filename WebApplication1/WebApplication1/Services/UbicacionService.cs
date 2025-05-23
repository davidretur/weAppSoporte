using WebAppInventarioS.Models;

namespace WebAppInventarioS.Services
{
    public class UbicacionService
    {
        private readonly HttpClient _httpClient;
        public UbicacionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
        }
        public async Task<List<Ubicacion>> GetAllUbicaciones()
        {
            return await _httpClient.GetFromJsonAsync<List<Ubicacion>>("api/Ubicacion");
        }
        public async Task<Ubicacion> GetUbicacionById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ubicacion>($"api/Ubicacion/{id}");
        }
        public async Task<Ubicacion> CreateUbicacion(Ubicacion ubicacion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ubicacion", ubicacion);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Ubicacion>();
        }
        public async Task UpdateUbicacion(int id, Ubicacion ubicacion)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Ubicacion/{id}", ubicacion);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteUbicacionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Ubicacion/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
