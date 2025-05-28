using WebAppInventarioS.Models;
using WebAppSoporte.Services;

namespace WebAppInventarioS.Services
{
    public class UsuariosService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;
        public UsuariosService(IHttpClientFactory httpClientFactory, AuthState authState)
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
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync("api/Usuario");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Usuario>>();
        }
        public async Task<Usuario> GetUsuarioById(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Usuario/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }
        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("api/Usuario", usuario);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }
        public async Task UpdateUsuario(int id, Usuario usuario)
        {
            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/Usuario/{id}", usuario);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateContracena(int id, string contracena)
        {
            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/Usuario/Contracena/{id}", contracena);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteUsuarioAsync(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"api/Usuario/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
