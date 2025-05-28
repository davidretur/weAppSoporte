using WebAppInventarioS.Models;
using WebAppSoporte.Services;

namespace WebAppInventarioS.Services
{
    public class RolService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;

        public RolService(IHttpClientFactory httpClientFactory, AuthState authState)
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
        // Get all articles
        public async Task<List<Rol>> GetRolesAsync()
        {
            AddJwtHeader();
            var response =  await _httpClient.GetAsync("api/Roles");
             if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Rol>>();

        }
        // Get a single article by ID
        public async Task<Rol> GetRolesAsync(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Roles/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rol>();
        }
        // Create a new Rol
        public async Task<Rol> CreateRolesAsync(Rol rol)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("api/Roles", rol);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rol>();
        }

        // Update an existing Rol
        public async Task UpdateRolesAsync(int id, Rol rol)
        {
            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/Roles/{id}", rol);
            response.EnsureSuccessStatusCode();
        }

        // Delete a Rol
        public async Task DeleteRolesAsync(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"api/Roles/{id}");
            response.EnsureSuccessStatusCode();
        }

        private async Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> action)
        {
            AddJwtHeader();
            var response = await action();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
