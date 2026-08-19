using Microsoft.AspNetCore.Authorization;
using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using BellezaMas.Api.Excepciones;
using BellezaMas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[Authorize(Roles = "Administrador,Usuario")]
[ApiController]
[Route("api/[controller]")]
public class MovimientosController : ControllerBase
{
    private readonly InventarioContext _ctx;
    private readonly ILogger<MovimientosController> _logger;

    public MovimientosController(
        InventarioContext ctx,
        ILogger<MovimientosController> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    // GET api/movimientos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimientoDto>>> Obtener(
        string? tipo,
        int pagina = 1,
        int tamano = 10)
    {
        if (pagina < 1)
            return BadRequest("La página debe ser mayor o igual a 1.");

        if (tamano is < 1 or > 100)
            return BadRequest("El tamaño debe estar entre 1 y 100.");

        var consulta = _ctx.MovimientosInventario
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            consulta = consulta.Where(m =>
                m.TipoMovimiento == tipo);
        }

        var movimientos = await consulta
            .OrderByDescending(m => m.Fecha)
            .ThenByDescending(m => m.Id)
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .Select(m => new MovimientoDto
            {
                Id = m.Id,
                ProductoId = m.ProductoId,
                ProductoNombre = m.Producto!.Nombre,
                TipoMovimiento = m.TipoMovimiento,
                Cantidad = m.Cantidad,
                Fecha = m.Fecha,
                Observacion = m.Observacion
            })
            .ToListAsync();

        return Ok(movimientos);
    }

    // POST api/movimientos
    [HttpPost]
    public async Task<ActionResult<MovimientoDto>> Crear(
        MovimientoCrearDto dto)
    {
        var producto = await _ctx.Productos
            .FindAsync(dto.ProductoId);

        if (producto is null)
            return BadRequest("El producto seleccionado no existe.");

        if (!producto.Activo)
            return BadRequest("El producto está inactivo.");

        string tipo = dto.TipoMovimiento.Trim();

        if (tipo != "Entrada" && tipo != "Salida")
        {
            return BadRequest(
                "El tipo de movimiento debe ser Entrada o Salida.");
        }

        try
        {
            if (tipo == "Entrada")
            {
                producto.Stock += dto.Cantidad;
            }
            else
            {
                if (dto.Cantidad > producto.Stock)
                {
                    throw new StockInsuficienteException(
                        $"Stock insuficiente para '{producto.Nombre}'. " +
                        $"Disponible: {producto.Stock}. " +
                        $"Solicitado: {dto.Cantidad}.");
                }

                producto.Stock -= dto.Cantidad;
            }

            var movimiento = new MovimientoInventario
            {
                ProductoId = producto.Id,
                TipoMovimiento = tipo,
                Cantidad = dto.Cantidad,
                Fecha = DateTime.UtcNow,
                Observacion = dto.Observacion
            };

            _ctx.MovimientosInventario.Add(movimiento);

            await _ctx.SaveChangesAsync();

            _logger.LogInformation(
                "Movimiento {Tipo} registrado para el producto {Producto}. Cantidad: {Cantidad}",
                tipo,
                producto.Nombre,
                dto.Cantidad);

            var respuesta = new MovimientoDto
            {
                Id = movimiento.Id,
                ProductoId = producto.Id,
                ProductoNombre = producto.Nombre,
                TipoMovimiento = movimiento.TipoMovimiento,
                Cantidad = movimiento.Cantidad,
                Fecha = movimiento.Fecha,
                Observacion = movimiento.Observacion
            };

            return CreatedAtAction(
                nameof(Obtener),
                respuesta);
        }
        catch (StockInsuficienteException ex)
        {
            _logger.LogWarning(
                ex,
                "Intento de salida con stock insuficiente para el producto {ProductoId}",
                dto.ProductoId);

            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al registrar movimiento para el producto {ProductoId}",
                dto.ProductoId);

            return StatusCode(
                500,
                "Ocurrió un error al registrar el movimiento.");
        }
    }
}