namespace WebAppSoporte.Services
{
    public class AuthState
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public int IdRol { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);
    }
}
