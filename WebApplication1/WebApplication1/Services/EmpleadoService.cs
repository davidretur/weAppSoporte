using WebAppInventarioS.Models;
using WebAppInventarioS.Models.Dtos;
using WebAppSoporte.Services;

namespace WebAppInventarioS.Services
{
    public class EmpleadoService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;
        public EmpleadoService(IHttpClientFactory httpClientFactory, AuthState authState)
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

        public async Task<List<Empleado>> GetAllEmpleadosU()
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync("api/Empleado");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Empleado>>();
        }
        public async Task<List<EmpleadoDto>> GetAllEmpleados(
            DepartamentoService departamentoService,
            UbicacionService ubicacionService)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync("api/Empleado");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            var empleados = await response.Content.ReadFromJsonAsync<List<Empleado>>();

            var departamentos = await departamentoService.GetAllDepartamentos();
            var ubicaciones = await ubicacionService.GetAllUbicaciones();

            var resultado = from e in empleados
                            join d in departamentos on e.IdDepartamento equals d.IdDepartamento
                            join u in ubicaciones on e.IdUbicacion equals u.IdUbicacion
                            select new EmpleadoDto
                            {
                                IdEmpleado = e.IdEmpleado,
                                Nombre = e.Nombre,
                                ApellidoP = e.ApellidoP,
                                ApellidoM = e.ApellidoM,
                                Puesto = e.Puesto,
                                UsuarioWindows = e.UsuarioWindows,
                                UsuarioAD = e.UsuarioAD,
                                Correo = e.Correo,
                                Pass = e.Pass,
                                NombreDepartamento = d.NombreDepartamento,
                                NombreUbicacion = u.Zona,
                                Status = e.Status
                            };

            return resultado.ToList();
        }
        public async Task<Empleado> GetEmpleadoByIdAsync(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Empleado/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Empleado>();
        }
        public async Task<Empleado> CreateEmpleadoAsync(Empleado empleado)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("api/Empleado", empleado);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Empleado>();
        }
        public async Task UpdateEmpleadoAsync(int id, Empleado empleado)
        {
            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/Empleado/{id}", empleado);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteEmpleadoAsync(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"api/Empleado/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
