namespace BibliotecaLab.Api.Dtos
{
    public class LibroDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Autor { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public int AnioPublicacion { get; set; }

        public bool Disponible { get; set; }
    }
}