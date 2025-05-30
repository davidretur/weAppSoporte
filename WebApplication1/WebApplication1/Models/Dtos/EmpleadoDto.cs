namespace WebAppInventarioS.Models.Dtos
{
    public class EmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Puesto { get; set; }
        public string UsuarioWindows { get; set; }
        public string UsuarioAD { get; set; }
        public string Correo { get; set; }
        public string Pass { get; set; }
        public string Acceso { get; set; }
        public string NombreDepartamento { get; set; }
        public string NombreUbicacion { get; set; }
        public bool Status { get; set; }
    }
}
