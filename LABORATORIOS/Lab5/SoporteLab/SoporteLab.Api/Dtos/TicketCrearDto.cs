using System.ComponentModel.DataAnnotations;

namespace SoporteLab.Api.Dtos
{
    public class TicketCrearDto
    {
        [Required(ErrorMessage = "El titulo es obligatorio")]
        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        public string Titulo { get; set; } = "";

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(1000, ErrorMessage = "Maximo 1000 caracteres")]
        public string Descripcion { get; set; } = "";

        public string Prioridad { get; set; } = "Media";

        // Opcional: el ticket puede nacer sin tecnico asignado.
        public int? TecnicoId { get; set; }
    }
}
