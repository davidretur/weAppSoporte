using WebAppInventarioS.Models;
using WebAppInventarioS.Models.Dtos;

namespace WebAppInventarioS.Services
{
    public class EmpleadoService
    {
        private readonly HttpClient _httpClient;
        public EmpleadoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
        }

        public async Task<List<Empleado>> GetAllEmpleadosU()
        {
            return await _httpClient.GetFromJsonAsync<List<Empleado>>("api/Empleado");
        }
        public async Task<List<EmpleadoDto>> GetAllEmpleados(
            DepartamentoService departamentoService,
            UbicacionService ubicacionService)
        {
            var empleados = await _httpClient.GetFromJsonAsync<List<Empleado>>("api/Empleado");
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
            return await _httpClient.GetFromJsonAsync<Empleado>($"api/Empleado/{id}");
        }
        public async Task<Empleado> CreateEmpleadoAsync(Empleado empleado)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Empleado", empleado);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Empleado>();
        }
        public async Task UpdateEmpleadoAsync(int id, Empleado empleado)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Empleado/{id}", empleado);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteEmpleadoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Empleado/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
