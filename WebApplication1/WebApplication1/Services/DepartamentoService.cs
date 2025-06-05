using WebAppInventarioS.Models;
using WebAppSoporte.Services;

namespace WebAppInventarioS.Services
{
    public class DepartamentoService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;
        public DepartamentoService(IHttpClientFactory httpClientFactory, AuthState authState)
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
        private async Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> action)
        {
            AddJwtHeader();
            var response = await action();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<List<Departamento>> GetAllDepartamentos()
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync("api/Departamento");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Departamento>>();
        }

        public async Task<Departamento> GetDepartamentoByIdAsync(int? id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Departamento/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Departamento>();
        }
        public async Task<Departamento> CreateDepartamentoAsync(Departamento departamento)
        {
            var response = await SendAsync(() => _httpClient.PostAsJsonAsync("api/Departamento", departamento));
            return await response.Content.ReadFromJsonAsync<Departamento>();
        }
        public async Task UpdateDepartamentoAsync(int id, Departamento departamento)
        {
            await SendAsync(() => _httpClient.PutAsJsonAsync($"api/Departamento/{id}", departamento));
        }
        public async Task DeleteDepartamentoAsync(int id)
        {
            await SendAsync(() => _httpClient.DeleteAsync($"api/Departamento/{id}"));
        }

    }
}
