using System.ComponentModel.DataAnnotations;

namespace CafeLab.Api.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    //Hash con sal y parametros; nunca la contraseña original.
    [Required]
    [StringLength(300)]
    public  string ContrasenaHash {  get; set; } = string.Empty;

    //"Barista" o "Administracion". El cliente nunca  escoge su rol.
    [Required]
    [StringLength(30)]
    public string Rol {  get; set; } = "Barista";


}
