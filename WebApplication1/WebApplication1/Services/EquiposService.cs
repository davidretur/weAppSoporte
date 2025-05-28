using WebAppInventarioS.Models;
using WebAppInventarioS.Models.Dtos;
using WebAppSoporte.Services;

namespace WebAppInventarioS.Services
{
    public class EquiposService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthState _authState;
        public EquiposService(IHttpClientFactory httpClientFactory, AuthState authState)
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
        public async Task<List<Equipo>> GetAllEquipos()
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync("api/Equipo");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Equipo>>();
        }
            public async Task<List<EquipoDto>> GetAllEquiposLink(
                DepartamentoService departamentoService,
                UbicacionService ubicacionService,
                string busqueda
                )
            {
            if (string.IsNullOrEmpty(busqueda))
            {
                busqueda = "abc";
            }
                AddJwtHeader();
                var response = await _httpClient.GetAsync($"api/Equipo/buscar/{busqueda}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();
                response.EnsureSuccessStatusCode();
                var equipos = await response.Content.ReadFromJsonAsync<List<Equipo>>();

                var departamentos = await departamentoService.GetAllDepartamentos();
                var ubicaciones = await ubicacionService.GetAllUbicaciones();

                var empleados = await _httpClient.GetAsync("api/Empleado");
                if (empleados.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();
                empleados.EnsureSuccessStatusCode();
                var empleadosList = await empleados.Content.ReadFromJsonAsync<List<Empleado>>();

            var resultado = from e in equipos
                join u in ubicaciones on e.IdUbicacion equals u.IdUbicacion into uj
                from u in uj.DefaultIfEmpty()
                join d in departamentos on e.IdDepartamento equals d.IdDepartamento into dj
                from d in dj.DefaultIfEmpty()
                join emp in empleadosList on e.IdEmpleado equals emp.IdEmpleado into ej
                from emp in ej.DefaultIfEmpty()
                where e.Status == true
                select new EquipoDto
                {
                    IdEquipo = e.IdEquipo,
                    NumeroSerie = e.NumeroSerie,
                    Marca = e.Marca,
                    Modelo = e.Modelo,
                    Ip = e.Ip,
                    Ram = e.Ram,
                    DiscoDuro = e.DiscoDuro,
                    Procesador = e.Procesador,
                    So = e.So,
                    EquipoEstatus = e.EquipoEstatus,
                    Empresa = e.Empresa,
                    Renovar = e.Renovar,
                    FechaUltimaCaptura = e.FechaUltimaCaptura,
                    FechaUltimoMantto = e.FechaUltimoMantto,
                    ElaboroResponsiva = e.ElaboroResponsiva,
                    Zona = u?.Zona ?? "Sin Ubicación",
                    NombreDepartamento = d?.NombreDepartamento ?? "Sin Departamento",
                    IdEmpleado = emp?.IdEmpleado ?? 1,
                    Status = e.Status
                };

                return resultado.ToList();
            }
        public async Task<Equipo> GetEquipoById(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Equipo/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Equipo>();
        }
        public async Task<Equipo> CreateEquipo(Equipo equipo)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("api/Equipo", equipo);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Equipo>();
        }
        public async Task UpdateEquipo(int id, Equipo equipo)
        {
            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/Equipo/{id}", equipo);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteEquipoAsync(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"api/Equipo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
