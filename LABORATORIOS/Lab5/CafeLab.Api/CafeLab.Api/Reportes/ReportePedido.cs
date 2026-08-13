using CafeLab.Api.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CafeLab.Api.Reportes;

public static class ReportePedidos
{
    public static byte[] Generar(
        List<PedidoDto> pedidos, DateTime desde, DateTime hasta)
    {
        return Document.Create(documento =>
        {
            documento.Page(pagina =>
            {
                pagina.Size(PageSizes.A4);
                pagina.Margin(2, Unit.Centimetre);
                pagina.DefaultTextStyle(t => t.FontSize(10));

                // ---------- Encabezado ----------
                pagina.Header().Column(col =>
                {
                    col.Item().Text("CafeLab - Reporte de pedidos")
                        .FontSize(18).SemiBold();

                    col.Item().Text(
                        $"Periodo del {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}")
                        .FontSize(10).FontColor(Colors.Grey.Darken1);
                });

                // ---------- Contenido ----------
                pagina.Content().PaddingVertical(12).Column(col =>
                {
                    col.Item().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(35); // Id
                            c.RelativeColumn(3); // Cliente
                            c.RelativeColumn(3); // Producto
                            c.ConstantColumn(45); // Cantidad
                            c.ConstantColumn(75); // Total
                            c.ConstantColumn(75); // Estado
                        });

                        tabla.Header(encabezado =>
                        {
                            encabezado.Cell().Text("Id").SemiBold();
                            encabezado.Cell().Text("Cliente").SemiBold();
                            encabezado.Cell().Text("Producto").SemiBold();
                            encabezado.Cell().Text("Cant.").SemiBold();
                            encabezado.Cell().Text("Total").SemiBold();
                            encabezado.Cell().Text("Estado").SemiBold();
                        });

                        foreach (var p in pedidos)
                        {
                            tabla.Cell().Text(p.Id.ToString());
                            tabla.Cell().Text(p.Cliente);
                            tabla.Cell().Text(p.ProductoNombre);
                            tabla.Cell().Text(p.Cantidad.ToString());
                            tabla.Cell().Text(p.Total.ToString("N2"));
                            tabla.Cell().Text(p.Estado);
                        }
                    });

                    col.Item().PaddingTop(14).AlignRight()
                        .Text($"Pedidos: {pedidos.Count} " +
                              $"Ventas: {pedidos.Sum(p => p.Total):N2} colones")
                        .SemiBold();
                });

                // ---------- Pie ----------
                pagina.Footer().AlignCenter().Text(texto =>
                {
                    texto.Span("Pagina ");
                    texto.CurrentPageNumber();
                    texto.Span(" de ");
                    texto.TotalPages();
                });
            });
        })
        .GeneratePdf();
    }
}