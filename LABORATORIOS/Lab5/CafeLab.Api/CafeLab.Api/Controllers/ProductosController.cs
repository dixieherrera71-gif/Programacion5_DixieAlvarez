using CafeLab.Api.Datos;
using CafeLab.Api.Dtos;
using CafeLab.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly CafeContext _ctx;

    public ProductosController(CafeContext ctx) => _ctx = ctx;

    // GET api/productos
    // GET api/productos?categoria=Bebida&soloDisponibles=true&pagina=1&tamano=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Obtener(
        string? categoria, bool soloDisponibles = false,
        int pagina = 1, int tamano = 10)
    {
        if (pagina < 1)
            return BadRequest("La pagina debe ser mayor o igual a 1.");

        if (tamano is < 1 or > 100)
            return BadRequest("El tamano debe estar entre 1 y 100.");

        long omitir = ((long)pagina - 1) * tamano;
        if (omitir > int.MaxValue)
            return BadRequest("El numero de pagina es demasiado grande.");

        // La consulta se arma pero todavia NO se ejecuta.
        var consulta = _ctx.Productos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoria))
            consulta = consulta.Where(p => p.Categoria == categoria);

        if (soloDisponibles)
            consulta = consulta.Where(p => p.Disponible);

        var lista = await consulta
            .OrderBy(p => p.Nombre)
            .ThenBy(p => p.Id) // orden totalmente unico
            .Skip((int)omitir)
            .Take(tamano)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Precio = p.Precio,
                Disponible = p.Disponible,
                VecesPedido = p.Pedidos.Count() // se resuelve en SQL
            })
            .ToListAsync(); // aqui viaja a la base

        return Ok(lista);
    }

    // GET api/productos/3
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
    {
        var dto = await _ctx.Productos
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Precio = p.Precio,
                Disponible = p.Disponible,
                VecesPedido = p.Pedidos.Count()
            })
            .FirstOrDefaultAsync();

        if (dto is null)
            return NotFound(); // 404: el recurso no existe

        return Ok(dto); // 200
    }

    // POST api/productos
    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<ActionResult<ProductoDto>> Crear(ProductoGuardarDto dto)
    {
        string nombre = dto.Nombre.Trim();

        // Regla de negocio: no dos productos con el mismo nombre.
        bool repetido = await _ctx.Productos
            .AnyAsync(p => p.Nombre == nombre);

        if (repetido)
            return Conflict($"Ya existe un producto llamado '{nombre}'.");

        var producto = new Producto
        {
            Nombre = nombre,
            Categoria = dto.Categoria,
            Precio = dto.Precio,
            Disponible = dto.Disponible
        };

        _ctx.Productos.Add(producto);

        try
        {
            await _ctx.SaveChangesAsync(); // la base asigna el Id
        }
        catch (DbUpdateException ex) when (
            ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            return Conflict($"Ya existe un producto llamado '{nombre}'.");
        }

        var respuesta = new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Categoria = producto.Categoria,
            Precio = producto.Precio,
            Disponible = producto.Disponible,
            VecesPedido = 0
        };

        // 201 Created + cabecera Location hacia api/productos/{nuevo id}
        return CreatedAtAction(nameof(ObtenerPorId),
            new { id = producto.Id }, respuesta);
    }

    // PUT api/productos/3
    [Authorize(Roles = "Administrador")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, ProductoGuardarDto dto)
    {
        var producto = await _ctx.Productos.FindAsync(id);

        if (producto is null)
            return NotFound();

        string nombre = dto.Nombre.Trim();

        bool repetido = await _ctx.Productos
            .AnyAsync(p => p.Id != id && p.Nombre == nombre);

        if (repetido)
            return Conflict($"Ya existe un producto llamado '{nombre}'.");

        // El objeto esta rastreado: basta con modificarlo y guardar.
        producto.Nombre = nombre;
        producto.Categoria = dto.Categoria;
        producto.Precio = dto.Precio;
        producto.Disponible = dto.Disponible;

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (
            ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            return Conflict($"Ya existe un producto llamado '{nombre}'.");
        }

        return NoContent(); // 204: exito sin cuerpo de respuesta
    }

    // DELETE api/productos/3
    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var producto = await _ctx.Productos.FindAsync(id);

        if (producto is null)
            return NotFound();

        // Un producto con historial de pedidos no se puede borrar.
        bool tienePedidos = await _ctx.Pedidos.AnyAsync(p => p.ProductoId == id);

        if (tienePedidos)
            return Conflict(
                "El producto tiene pedidos registrados. " +
                "Marquelo como no disponible en lugar de eliminarlo.");

        _ctx.Productos.Remove(producto);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}