using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly InventarioContext _ctx;

    public DashboardController(InventarioContext ctx)
    {
        _ctx = ctx;
    }

    // GET api/dashboard
    [HttpGet]
    public async Task<ActionResult<DashboardDto>> Obtener()
    {
        var respuesta = new DashboardDto
        {
            TotalProductos = await _ctx.Productos
                .CountAsync(),

            TotalCategorias = await _ctx.Categorias
                .CountAsync(),

            TotalProveedores = await _ctx.Proveedores
                .CountAsync(),

            StockBajo = await _ctx.Productos
                .CountAsync(p =>
                    p.Stock <= p.StockMinimo),

            ProductosRecientes = await _ctx.Productos
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .Take(5)
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

                    StockBajo =
                        p.Stock <= p.StockMinimo
                })
                .ToListAsync()
        };

        return Ok(respuesta);
    }
}