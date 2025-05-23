using WebAppInventarioS.Models;

namespace WebAppInventarioS.Services
{
    public class DepartamentoService
    {
        private readonly HttpClient _httpClient;
        public DepartamentoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
        }

        public async Task<List<Departamento>> GetAllDepartamentos()
        {
            return await _httpClient.GetFromJsonAsync<List<Departamento>>("api/Departamento");
        }

        public async Task<Departamento> GetDepartamentoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Departamento>($"api/Departamento/{id}");
        }
        public async Task<Departamento> CreateDepartamentoAsync(Departamento departamento)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Departamento", departamento);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Departamento>();
        }
        public async Task UpdateDepartamentoAsync(int id, Departamento departamento)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Departamento/{id}", departamento);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteDepartamentoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Departamento/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
