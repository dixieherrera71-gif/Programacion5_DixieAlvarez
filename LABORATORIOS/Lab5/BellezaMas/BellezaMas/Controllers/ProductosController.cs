using Microsoft.AspNetCore.Authorization;
using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using BellezaMas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly InventarioContext _ctx;
    private readonly ILogger<ProductosController> _logger;

    public ProductosController(
        InventarioContext ctx,
        ILogger<ProductosController> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    // GET api/productos
    // GET api/productos?buscar=serum&categoriaId=1&orden=nombre&pagina=1&tamano=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Obtener(
        string? buscar,
        int? categoriaId,
        string orden = "nombre",
        int pagina = 1,
        int tamano = 10)
    {
        if (pagina < 1)
            return BadRequest("La página debe ser mayor o igual a 1.");

        if (tamano is < 1 or > 100)
            return BadRequest("El tamaño debe estar entre 1 y 100.");

        var consulta = _ctx.Productos
            .AsNoTracking()
            .AsQueryable();

        // FILTRO POR TEXTO
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            consulta = consulta.Where(p =>
                p.Nombre.Contains(buscar));
        }

        // FILTRO POR CATEGORÍA
        if (categoriaId.HasValue)
        {
            consulta = consulta.Where(p =>
                p.CategoriaId == categoriaId.Value);
        }

        // ORDENAMIENTO
        consulta = orden.ToLower() switch
        {
            "precio" => consulta.OrderBy(p => p.Precio),
            "stock" => consulta.OrderBy(p => p.Stock),
            _ => consulta.OrderBy(p => p.Nombre)
        };

        // PAGINACIÓN
        var lista = await consulta
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                StockMinimo = p.StockMinimo,
                Activo = p.Activo,

                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categoria!.Nombre,

                ProveedorId = p.ProveedorId,
                ProveedorNombre = p.Proveedor!.Nombre,

                StockBajo = p.Stock <= p.StockMinimo
            })
            .ToListAsync();

        return Ok(lista);
    }

    // GET api/productos/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
    {
        var producto = await _ctx.Productos
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                StockMinimo = p.StockMinimo,
                Activo = p.Activo,

                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categoria!.Nombre,

                ProveedorId = p.ProveedorId,
                ProveedorNombre = p.Proveedor!.Nombre,

                StockBajo = p.Stock <= p.StockMinimo
            })
            .FirstOrDefaultAsync();

        if (producto is null)
            return NotFound();

        return Ok(producto);
    }


    [Authorize(Roles = "Administrador")]
    // POST api/productos
    [HttpPost]
    public async Task<ActionResult<ProductoDto>> Crear(
        ProductoGuardarDto dto)
    {
        string nombre = dto.Nombre.Trim();

        bool repetido = await _ctx.Productos
            .AnyAsync(p => p.Nombre == nombre);

        if (repetido)
            return Conflict(
                $"Ya existe un producto llamado '{nombre}'.");

        bool categoriaExiste = await _ctx.Categorias
            .AnyAsync(c => c.Id == dto.CategoriaId);

        if (!categoriaExiste)
            return BadRequest("La categoría seleccionada no existe.");

        bool proveedorExiste = await _ctx.Proveedores
            .AnyAsync(p => p.Id == dto.ProveedorId);

        if (!proveedorExiste)
            return BadRequest("El proveedor seleccionado no existe.");

        var producto = new Producto
        {
            Nombre = nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Stock = dto.Stock,
            StockMinimo = dto.StockMinimo,
            CategoriaId = dto.CategoriaId,
            ProveedorId = dto.ProveedorId,
            Activo = dto.Activo
        };

        _ctx.Productos.Add(producto);

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (
            ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            return Conflict(
                $"Ya existe un producto llamado '{nombre}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al crear el producto {Producto}",
                nombre);

            return StatusCode(
                500,
                "Ocurrió un error al guardar el producto.");
        }

        var respuesta = await _ctx.Productos
            .AsNoTracking()
            .Where(p => p.Id == producto.Id)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                StockMinimo = p.StockMinimo,
                Activo = p.Activo,

                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categoria!.Nombre,

                ProveedorId = p.ProveedorId,
                ProveedorNombre = p.Proveedor!.Nombre,

                StockBajo = p.Stock <= p.StockMinimo
            })
            .FirstAsync();

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = producto.Id },
            respuesta);
    }

    [Authorize(Roles = "Administrador")]
    // PUT api/productos/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ProductoGuardarDto dto)
    {
        var producto = await _ctx.Productos.FindAsync(id);

        if (producto is null)
            return NotFound();

        string nombre = dto.Nombre.Trim();

        bool repetido = await _ctx.Productos
            .AnyAsync(p => p.Id != id && p.Nombre == nombre);

        if (repetido)
            return Conflict(
                $"Ya existe un producto llamado '{nombre}'.");

        bool categoriaExiste = await _ctx.Categorias
            .AnyAsync(c => c.Id == dto.CategoriaId);

        if (!categoriaExiste)
            return BadRequest("La categoría seleccionada no existe.");

        bool proveedorExiste = await _ctx.Proveedores
            .AnyAsync(p => p.Id == dto.ProveedorId);

        if (!proveedorExiste)
            return BadRequest("El proveedor seleccionado no existe.");

        producto.Nombre = nombre;
        producto.Descripcion = dto.Descripcion;
        producto.Precio = dto.Precio;
        producto.Stock = dto.Stock;
        producto.StockMinimo = dto.StockMinimo;
        producto.CategoriaId = dto.CategoriaId;
        producto.ProveedorId = dto.ProveedorId;
        producto.Activo = dto.Activo;

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al actualizar el producto {Id}",
                id);

            return StatusCode(
                500,
                "Ocurrió un error al actualizar el producto.");
        }

        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    // DELETE api/productos/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var producto = await _ctx.Productos.FindAsync(id);

        if (producto is null)
            return NotFound();

        bool tieneMovimientos = await _ctx.MovimientosInventario
            .AnyAsync(m => m.ProductoId == id);

        if (tieneMovimientos)
        {
            return Conflict(
                "El producto tiene movimientos registrados. " +
                "Debe desactivarlo en lugar de eliminarlo.");
        }

        _ctx.Productos.Remove(producto);

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al eliminar el producto {Id}",
                id);

            return StatusCode(
                500,
                "Ocurrió un error al eliminar el producto.");
        }

        return NoContent();
    }
}