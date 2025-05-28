using WebAppInventarioS.Services;
using WebAppSoporte.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Configure HttpClient with the base address of your Web API
builder.Services.AddHttpClient("InventarioApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5076/"); // Replace with your API's base URL
});

builder.Services.AddScoped<RolService>(); // Register RolService for dependency injection
builder.Services.AddScoped<DepartamentoService>(); // Register DepartamentoService for dependency injection
builder.Services.AddScoped<EmpleadoService>(); // Register EmpleadoService for dependency injection
builder.Services.AddScoped<UbicacionService>(); // Register UbicacionService for dependency injection
builder.Services.AddScoped<UsuariosService>(); // Register UsuariosService for dependency injection
builder.Services.AddScoped<EquiposService>(); // Register EquiposService for dependency injection
builder.Services.AddScoped<LicenciaOfficeService>(); // Register LicenciaOfficeService for dependency injection
builder.Services.AddScoped<AuthService>(); // Register AuthService for authentication
builder.Services.AddSingleton<AuthState>(); // Register AuthState as a singleton for authentication state management

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
