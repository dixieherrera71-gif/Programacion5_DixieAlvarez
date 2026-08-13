using CafeLab.Api.Datos;
using CafeLab.Api.Dtos;
using CafeLab.Api.Reportes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class ReportesController : ControllerBase
{
    private readonly CafeContext _ctx;

    public ReportesController(CafeContext ctx) => _ctx = ctx;

    // GET api/reportes/pedidos?desde=2026-08-01&hasta=2026-08-31
    [HttpGet("pedidos")]
    public async Task<IActionResult> PedidosPdf(DateTime? desde, DateTime? hasta)
    {
        // Valores por omision: el mes en curso.
        var inicio = desde ?? new DateTime(
            DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1,
            0, 0, 0, DateTimeKind.Utc);

        var fin = hasta ?? DateTime.UtcNow;

        if (inicio > fin)
            return BadRequest("La fecha inicial no puede ser mayor que la final.");

        // El fin del dia, para incluir los pedidos de la fecha final.
        var finInclusivo = fin.Date.AddDays(1).AddTicks(-1);

        var pedidos = await _ctx.Pedidos
            .AsNoTracking()
            .Where(p => p.FechaHora >= inicio.Date && p.FechaHora <= finInclusivo)
            .OrderBy(p => p.FechaHora)
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

        byte[] archivo = ReportePedidos.Generar(pedidos, inicio, finInclusivo);

        // El tercer argumento produce la cabecera Content-Disposition
        // con el nombre sugerido de descarga.
        return File(archivo, "application/pdf",
            $"pedidos-{inicio:yyyyMMdd}-{fin:yyyyMMdd}.pdf");
    }
}