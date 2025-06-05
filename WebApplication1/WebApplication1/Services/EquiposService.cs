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
        public async Task<EquipoDto?> GetAllEquiposLink(
            DepartamentoService departamentoService,
            UbicacionService ubicacionService,
            string busqueda
        )
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Equipo/buscar/{busqueda}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            var equipo = await response.Content.ReadFromJsonAsync<Equipo>();

            // Si no hay coincidencia, equipo será null o tendrá valores por defecto
            if (equipo == null || equipo.IdEquipo == 0)
                return null;

            // Obtén solo los datos relacionados por ID
            if (equipo.IdDepartamento != null && equipo.IdEmpleado != null )
            {
                var departamento = await departamentoService.GetDepartamentoByIdAsync(equipo.IdDepartamento);
                var ubicacion = await ubicacionService.GetUbicacionById(equipo.IdUbicacion);

                var empleadoResponse = await _httpClient.GetAsync($"api/Empleado/{equipo.IdEmpleado}");
                if (empleadoResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();
                empleadoResponse.EnsureSuccessStatusCode();
                var empleado = await empleadoResponse.Content.ReadFromJsonAsync<Empleado>();

                return new EquipoDto
                {
                    IdEquipo = equipo.IdEquipo,
                    NumeroSerie = equipo.NumeroSerie,
                    Etiqueta = equipo.Etiqueta,
                    Marca = equipo.Marca,
                    Modelo = equipo.Modelo,
                    Ip = equipo.Ip,
                    Ram = equipo.Ram,
                    DiscoDuro = equipo.DiscoDuro,
                    Procesador = equipo.Procesador,
                    So = equipo.So,
                    EquipoEstatus = equipo.EquipoEstatus,
                    Empresa = equipo.Empresa,
                    Renovar = equipo.Renovar,
                    FechaUltimaCaptura = equipo.FechaUltimaCaptura,
                    FechaUltimoMantto = equipo.FechaUltimoMantto,
                    ElaboroResponsiva = equipo.ElaboroResponsiva,
                    Zona = ubicacion?.Zona ?? "Sin Ubicación",
                    NombreDepartamento = departamento?.NombreDepartamento ?? "Sin Departamento",
                    IdEmpleado = empleado?.IdEmpleado ?? 0,
                    Status = equipo.Status
                };
            }
            else
            {
                return new EquipoDto
                {
                    IdEquipo = equipo.IdEquipo,
                    NumeroSerie = equipo.NumeroSerie,
                    Etiqueta = equipo.Etiqueta,
                    Marca = equipo.Marca,
                    Modelo = equipo.Modelo,
                    Ip = equipo.Ip,
                    Ram = equipo.Ram,
                    DiscoDuro = equipo.DiscoDuro,
                    Procesador = equipo.Procesador,
                    So = equipo.So,
                    EquipoEstatus = equipo.EquipoEstatus,
                    Empresa = equipo.Empresa,
                    Renovar = equipo.Renovar,
                    FechaUltimaCaptura = equipo.FechaUltimaCaptura,
                    FechaUltimoMantto = equipo.FechaUltimoMantto,
                    ElaboroResponsiva = equipo.ElaboroResponsiva,
                    Zona =  "Sin Ubicación",
                    NombreDepartamento =  "Sin Departamento",
                    IdEmpleado = 0,
                    Status = equipo.Status
                };
            }
           
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
        public async Task<List<Equipo>> GetEquipoEmpleadoById(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync($"api/Equipo/Empleado/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            var equipos = await response.Content.ReadFromJsonAsync<List<Equipo>>();
            return equipos ?? new List<Equipo>();
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
