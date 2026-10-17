using Microsoft.AspNetCore.Mvc;
using BibliotecaLab.Api.Models;
using BibliotecaLab.Api.Dtos;

namespace BibliotecaLab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private static List<Libro> _libros = new List<Libro>
        {
            new Libro
            {
                Id = 1,
                Titulo = "Cien años de soledad",
                Autor = "Gabriel García Márquez",
                Isbn = "9780307474728",
                AnioPublicacion = 1967,
                Disponible = true
            },

            new Libro
            {
                Id = 2,
                Titulo = "El principito",
                Autor = "Antoine de Saint-Exupéry",
                Isbn = "9780156013987",
                AnioPublicacion = 1943,
                Disponible = true
            },

            new Libro
            {
                Id = 3,
                Titulo = "Don Quijote de la Mancha",
                Autor = "Miguel de Cervantes",
                Isbn = "9788420412146",
                AnioPublicacion = 1605,
                Disponible = true
            }
        };

        // GET: api/libros
        [HttpGet]
        public ActionResult<IEnumerable<LibroDto>> GetLibros(
            bool? disponible = null,
            int pagina = 1,
            int tamano = 5)
        {
            var consulta = _libros.AsQueryable();

            if (disponible.HasValue)
            {
                consulta = consulta.Where(
                    l => l.Disponible == disponible.Value
                );
            }

            var resultado = consulta
                .OrderBy(l => l.Titulo)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .Select(l => new LibroDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    Isbn = l.Isbn,
                    AnioPublicacion = l.AnioPublicacion,
                    Disponible = l.Disponible
                })
                .ToList();

            return Ok(resultado);
        }

        // GET: api/libros/1
        [HttpGet("{id}")]
        public ActionResult<LibroDto> GetLibroPorId(int id)
        {
            var libro = _libros.FirstOrDefault(l => l.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            var libroDto = new LibroDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Isbn = libro.Isbn,
                AnioPublicacion = libro.AnioPublicacion,
                Disponible = libro.Disponible
            };

            return Ok(libroDto);
        }

        // POST: api/libros
        [HttpPost]
        public ActionResult<LibroDto> CrearLibro(
            LibroCrearDto libroCrearDto)
        {
            var isbnExiste = _libros.Any(
                l => l.Isbn == libroCrearDto.Isbn
            );

            if (isbnExiste)
            {
                return BadRequest(
                    "Ya existe un libro con ese ISBN."
                );
            }

            var nuevoId = _libros.Any()
                ? _libros.Max(l => l.Id) + 1
                : 1;

            var nuevoLibro = new Libro
            {
                Id = nuevoId,
                Titulo = libroCrearDto.Titulo,
                Autor = libroCrearDto.Autor,
                Isbn = libroCrearDto.Isbn,
                AnioPublicacion = libroCrearDto.AnioPublicacion,
                Disponible = true
            };

            _libros.Add(nuevoLibro);

            var libroDto = new LibroDto
            {
                Id = nuevoLibro.Id,
                Titulo = nuevoLibro.Titulo,
                Autor = nuevoLibro.Autor,
                Isbn = nuevoLibro.Isbn,
                AnioPublicacion = nuevoLibro.AnioPublicacion,
                Disponible = nuevoLibro.Disponible
            };

            return CreatedAtAction(
                nameof(GetLibroPorId),
                new { id = nuevoLibro.Id },
                libroDto
            );
        }
    }
}