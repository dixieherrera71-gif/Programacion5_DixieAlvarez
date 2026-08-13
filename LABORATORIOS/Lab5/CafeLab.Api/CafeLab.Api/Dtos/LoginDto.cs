using System.ComponentModel.DataAnnotations;

namespace CafeLab.Api.Dtos;

public class LoginDto
{
    [Required, EmailAddress]
    public string Correo { get; set; } = "";

    [Required]
    public string Contrasena { get; set; } = "";
}