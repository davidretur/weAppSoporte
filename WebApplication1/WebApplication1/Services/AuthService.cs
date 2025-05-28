using System.Text.Json;
using WebAppInventarioS.Models;

namespace WebAppSoporte.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;

        public AuthService(IHttpClientFactory httpClientFactory, AuthState authState)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
            _authState = authState;
        }
        public async Task<bool> AuthenticateUserAsync(string usuario, string contracena)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/", new { Usuario = usuario, Contracena = contracena });
            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            if (json.TryGetProperty("token", out var tokenProp) &&
                json.TryGetProperty("idRol", out var idRolProp) &&
                json.TryGetProperty("data", out var dataProp) &&
                dataProp.TryGetProperty("user", out var userProp) &&
                userProp.TryGetProperty("id", out var idProp))
            {
                _authState.Token = tokenProp.GetString();
                _authState.Id = idProp.GetInt32();
                _authState.IdRol = idRolProp.GetInt32();
                return true;
            }
            return false;
        }
    }
}
