using Microsoft.AspNetCore.Authorization;
using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using BellezaMas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly InventarioContext _ctx;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(
        InventarioContext ctx,
        ILogger<CategoriasController> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    // GET api/categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> Obtener()
    {
        var categorias = await _ctx.Categorias
            .AsNoTracking()
            .OrderBy(c => c.Nombre)
            .Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Activo = c.Activo,
                CantidadProductos = c.Productos.Count()
            })
            .ToListAsync();

        return Ok(categorias);
    }

    // GET api/categorias/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaDto>> ObtenerPorId(int id)
    {
        var categoria = await _ctx.Categorias
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Activo = c.Activo,
                CantidadProductos = c.Productos.Count()
            })
            .FirstOrDefaultAsync();

        if (categoria is null)
            return NotFound();

        return Ok(categoria);
    }

    [Authorize(Roles = "Administrador")]
    // POST api/categorias
    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Crear(
        CategoriaGuardarDto dto)
    {
        string nombre = dto.Nombre.Trim();

        bool repetida = await _ctx.Categorias
            .AnyAsync(c => c.Nombre == nombre);

        if (repetida)
            return Conflict(
                $"Ya existe una categoría llamada '{nombre}'.");

        var categoria = new Categoria
        {
            Nombre = nombre,
            Descripcion = dto.Descripcion,
            Activo = dto.Activo
        };

        _ctx.Categorias.Add(categoria);

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al crear la categoría {Categoria}",
                nombre);

            return StatusCode(
                500,
                "Ocurrió un error al guardar la categoría.");
        }

        var respuesta = new CategoriaDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            Activo = categoria.Activo,
            CantidadProductos = 0
        };

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = categoria.Id },
            respuesta);
    }

    [Authorize(Roles = "Administrador")]
    // PUT api/categorias/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        CategoriaGuardarDto dto)
    {
        var categoria = await _ctx.Categorias.FindAsync(id);

        if (categoria is null)
            return NotFound();

        string nombre = dto.Nombre.Trim();

        bool repetida = await _ctx.Categorias
            .AnyAsync(c => c.Id != id && c.Nombre == nombre);

        if (repetida)
            return Conflict(
                $"Ya existe una categoría llamada '{nombre}'.");

        categoria.Nombre = nombre;
        categoria.Descripcion = dto.Descripcion;
        categoria.Activo = dto.Activo;

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al actualizar la categoría {Id}",
                id);

            return StatusCode(
                500,
                "Ocurrió un error al actualizar la categoría.");
        }

        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    // DELETE api/categorias/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var categoria = await _ctx.Categorias.FindAsync(id);

        if (categoria is null)
            return NotFound();

        bool tieneProductos = await _ctx.Productos
            .AnyAsync(p => p.CategoriaId == id);

        if (tieneProductos)
        {
            return Conflict(
                "La categoría tiene productos asociados. " +
                "Debe desactivarla en lugar de eliminarla.");
        }

        _ctx.Categorias.Remove(categoria);

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al eliminar la categoría {Id}",
                id);

            return StatusCode(
                500,
                "Ocurrió un error al eliminar la categoría.");
        }

        return NoContent();
    }
}