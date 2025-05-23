using WebAppInventarioS.Models;

namespace WebAppInventarioS.Services
{
    public class UsuariosService
    {
        private readonly HttpClient _httpClient;
        public UsuariosService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
        }
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("api/Usuario");
        }
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"api/Usuario/{id}");
        }
        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuario", usuario);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }
        public async Task UpdateUsuario(int id, Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Usuario/{id}", usuario);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteUsuarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Usuario/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
