using WebAppInventarioS.Models;

namespace WebAppInventarioS.Services
{
    public class RolService
    {
        private readonly HttpClient _httpClient;

        public RolService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
        }
        // Get all articles
        public async Task<List<Rol>> GetRolesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Rol>>("api/Roles");
        }
        // Get a single article by ID
        public async Task<Rol> GetRolesAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Roles/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rol>();
        }
        // Create a new article
        public async Task<Rol> CreateRolesAsync(Rol rol)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Roles", rol);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rol>();
        }

        // Update an existing article
        public async Task UpdateRolesAsync(int id, Rol rol)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Roles/{id}", rol);
            response.EnsureSuccessStatusCode();
        }

        // Delete an article
        public async Task DeleteRolesAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Roles/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
