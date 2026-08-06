namespace SeguridadDemo2.Dtos
{
    public class TokenDto
    {

        public string Token { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public  DateTime Expira { get; set; } 

    }
}
