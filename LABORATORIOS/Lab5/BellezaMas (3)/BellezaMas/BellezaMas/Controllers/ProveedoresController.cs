using Microsoft.AspNetCore.Authorization;
using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly InventarioContext _ctx;

    public ProveedoresController(InventarioContext ctx)
    {
        _ctx = ctx;
    }

    // GET api/proveedores
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Obtener()
    {
        var proveedores = await _ctx.Proveedores
            .AsNoTracking()
            .Where(p => p.Activo)
            .OrderBy(p => p.Nombre)
            .Select(p => new ProveedorDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Direccion = p.Direccion,
                Activo = p.Activo
            })
            .ToListAsync();

        return Ok(proveedores);
    }

    // GET api/proveedores/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProveedorDto>> ObtenerPorId(int id)
    {
        var proveedor = await _ctx.Proveedores
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProveedorDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Direccion = p.Direccion,
                Activo = p.Activo
            })
            .FirstOrDefaultAsync();

        if (proveedor is null)
            return NotFound();

        return Ok(proveedor);
    }
}