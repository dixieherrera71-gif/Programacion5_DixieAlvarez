using CafeLab.Api.Datos;
using CafeLab.Api.Dtos;
using CafeLab.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly CafeContext _ctx;

    public PedidosController(CafeContext ctx) => _ctx = ctx;

    // GET api/pedidos?estado=Recibido&pagina=1&tamano=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> Obtener(
        string? estado, int pagina = 1, int tamano = 10)
    {
        if (pagina < 1)
            return BadRequest("La pagina debe ser mayor o igual a 1.");

        if (tamano is < 1 or > 100)
            return BadRequest("El tamano debe estar entre 1 y 100.");

        long omitir = ((long)pagina - 1) * tamano;

        if (omitir > int.MaxValue)
            return BadRequest("El numero de pagina es demasiado grande.");

        var consulta = _ctx.Pedidos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(estado))
            consulta = consulta.Where(p => p.Estado == estado);

        var lista = await consulta
            .OrderByDescending(p => p.FechaHora)
            .ThenByDescending(p => p.Id)
            .Skip((int)omitir)
            .Take(tamano)
            .Select(p => new PedidoDto
            {
                Id = p.Id,
                Cliente = p.Cliente,
                ProductoNombre = p.Producto!.Nombre,
                Cantidad = p.Cantidad,
                Total = p.Total,
                Estado = p.Estado,
                FechaHora = p.FechaHora
            })
            .ToListAsync();

        return Ok(lista);
    }

    // GET api/pedidos/7
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PedidoDto>> ObtenerPorId(int id)
    {
        var dto = await _ctx.Pedidos
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PedidoDto
            {
                Id = p.Id,
                Cliente = p.Cliente,
                ProductoNombre = p.Producto!.Nombre,
                Cantidad = p.Cantidad,
                Total = p.Total,
                Estado = p.Estado,
                FechaHora = p.FechaHora
            })
            .FirstOrDefaultAsync();

        return dto is null ? NotFound() : Ok(dto);
    }

    // POST api/pedidos
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<PedidoDto>> Crear(PedidoCrearDto dto)
    {
        // 1. El producto debe existir. Si no, es culpa del cliente: 400, no 500.
        var producto = await _ctx.Productos.FindAsync(dto.ProductoId);

        if (producto is null)
            return BadRequest($"No existe un producto con Id {dto.ProductoId}.");

        // 2. Y debe estar disponible.
        if (!producto.Disponible)
            return BadRequest($"El producto '{producto.Nombre}' no esta disponible.");

        // 3. El servidor calcula el total con el precio real.
        var pedido = new Pedido
        {
            Cliente = dto.Cliente,
            ProductoId = producto.Id,
            Cantidad = dto.Cantidad,
            Total = producto.Precio * dto.Cantidad,
            Estado = "Recibido",
            FechaHora = DateTime.UtcNow
        };

        _ctx.Pedidos.Add(pedido);
        await _ctx.SaveChangesAsync();

        var respuesta = new PedidoDto
        {
            Id = pedido.Id,
            Cliente = pedido.Cliente,
            ProductoNombre = producto.Nombre,
            Cantidad = pedido.Cantidad,
            Total = pedido.Total,
            Estado = pedido.Estado,
            FechaHora = pedido.FechaHora
        };

        return CreatedAtAction(nameof(ObtenerPorId),
            new { id = pedido.Id }, respuesta);
    }
    // PUT api/pedidos/7/estado
    [Authorize(Roles = "Barista,Administrador")]
    [HttpPut("{id:int}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, PedidoEstadoDto dto)
    {
        var pedido = await _ctx.Pedidos.FindAsync(id);

        if (pedido is null)
            return NotFound();

        string? siguiente = pedido.Estado switch
        {
            "Recibido" => "Preparando",
            "Preparando" => "Entregado",
            _ => null
        };

        if (siguiente is null)
            return Conflict("El pedido ya fue entregado y no puede retroceder.");

        if (!string.Equals(dto.Estado, siguiente,
            StringComparison.Ordinal))
            return Conflict(
                $"El pedido solo puede avanzar de '{pedido.Estado}' " +
                $"a '{siguiente}'.");

        pedido.Estado = siguiente;
        await _ctx.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/pedidos/7
    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var pedido = await _ctx.Pedidos.FindAsync(id);

        if (pedido is null)
            return NotFound();

        // Un pedido ya entregado es historial: no se borra.
        if (pedido.Estado == "Entregado")
            return Conflict("Un pedido entregado no se puede eliminar.");

        _ctx.Pedidos.Remove(pedido);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}