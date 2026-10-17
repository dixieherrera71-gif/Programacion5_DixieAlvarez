using System.ComponentModel.DataAnnotations;

namespace BibliotecaLab.Api.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Autor { get; set; } = string.Empty;

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string Isbn { get; set; } = string.Empty;

        [Range(1500, 2026)]
        public int AnioPublicacion { get; set; }

        public bool Disponible { get; set; }
    }
}

