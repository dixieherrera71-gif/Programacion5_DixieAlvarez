using BellezaMas.Api.Datos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BellezaMas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class ReportesController : ControllerBase
{
    private readonly InventarioContext _ctx;
    private readonly ILogger<ReportesController> _logger;

    public ReportesController(
        InventarioContext ctx,
        ILogger<ReportesController> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    // GET api/reportes/movimientos?desde=2026-08-01&hasta=2026-08-31
    [HttpGet("movimientos")]
    public async Task<IActionResult> Movimientos(
        DateTime? desde,
        DateTime? hasta)
    {
        if (!desde.HasValue || !hasta.HasValue)
        {
            return BadRequest(
                "Debe indicar las fechas desde y hasta.");
        }

        if (desde.Value.Date > hasta.Value.Date)
        {
            return BadRequest(
                "La fecha inicial no puede ser mayor que la fecha final.");
        }

        DateTime fechaInicial = desde.Value.Date;
        DateTime fechaFinal = hasta.Value.Date.AddDays(1);

        var movimientos = await _ctx.MovimientosInventario
            .AsNoTracking()
            .Where(m =>
                m.Fecha >= fechaInicial &&
                m.Fecha < fechaFinal)
            .OrderBy(m => m.Fecha)
            .Select(m => new
            {
                m.Fecha,
                Producto = m.Producto!.Nombre,
                m.TipoMovimiento,
                m.Cantidad,
                m.Observacion
            })
            .ToListAsync();

        try
        {
            byte[] pdf = Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4);
                    pagina.Margin(35);
                    pagina.DefaultTextStyle(
                        x => x.FontSize(10));

                    pagina.Header()
                        .Column(columna =>
                        {
                            columna.Item()
                                .Text("Belleza & Más")
                                .FontSize(22)
                                .Bold()
                                .FontColor("#D94F8A");

                            columna.Item()
                                .Text("Reporte de movimientos de inventario")
                                .FontSize(14)
                                .Bold();

                            columna.Item()
                                .Text(
                                    $"Periodo: {desde.Value:dd/MM/yyyy} - {hasta.Value:dd/MM/yyyy}")
                                .FontSize(10)
                                .FontColor(Colors.Grey.Darken1);
                        });

                    pagina.Content()
                        .PaddingVertical(20)
                        .Column(columna =>
                        {
                            if (movimientos.Count == 0)
                            {
                                columna.Item()
                                    .Text(
                                        "No existen movimientos en el periodo seleccionado.")
                                    .FontSize(11);

                                return;
                            }

                            columna.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columnas =>
                                {
                                    columnas.RelativeColumn(2);
                                    columnas.RelativeColumn(3);
                                    columnas.RelativeColumn(2);
                                    columnas.RelativeColumn(1);
                                });

                                tabla.Header(encabezado =>
                                {
                                    encabezado.Cell()
                                        .Element(EstiloEncabezado)
                                        .Text("Fecha");

                                    encabezado.Cell()
                                        .Element(EstiloEncabezado)
                                        .Text("Producto");

                                    encabezado.Cell()
                                        .Element(EstiloEncabezado)
                                        .Text("Tipo");

                                    encabezado.Cell()
                                        .Element(EstiloEncabezado)
                                        .Text("Cantidad");
                                });

                                foreach (var movimiento in movimientos)
                                {
                                    tabla.Cell()
                                        .Element(EstiloCelda)
                                        .Text(
                                            movimiento.Fecha.ToString(
                                                "dd/MM/yyyy"));

                                    tabla.Cell()
                                        .Element(EstiloCelda)
                                        .Text(movimiento.Producto);

                                    tabla.Cell()
                                        .Element(EstiloCelda)
                                        .Text(movimiento.TipoMovimiento);

                                    tabla.Cell()
                                        .Element(EstiloCelda)
                                        .Text(
                                            movimiento.Cantidad.ToString());
                                }
                            });

                            int entradas = movimientos
                                .Where(m =>
                                    m.TipoMovimiento == "Entrada")
                                .Sum(m => m.Cantidad);

                            int salidas = movimientos
                                .Where(m =>
                                    m.TipoMovimiento == "Salida")
                                .Sum(m => m.Cantidad);

                            columna.Item()
                                .PaddingTop(20)
                                .Text($"Total de entradas: {entradas}")
                                .Bold();

                            columna.Item()
                                .Text($"Total de salidas: {salidas}")
                                .Bold();
                        });

                    pagina.Footer()
                        .AlignCenter()
                        .Text(texto =>
                        {
                            texto.Span("Belleza & Más - Página ");
                            texto.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"Movimientos_{desde.Value:yyyyMMdd}_{hasta.Value:yyyyMMdd}.pdf");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al generar el reporte PDF.");

            return StatusCode(
                500,
                "Ocurrió un error al generar el reporte.");
        }
    }

    private static IContainer EstiloEncabezado(IContainer container)
    {
        return container
            .Background("#FCE7F0")
            .BorderBottom(1)
            .BorderColor("#D94F8A")
            .Padding(6);
    }

    private static IContainer EstiloCelda(IContainer container)
    {
        return container
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .Padding(6);
    }
}
