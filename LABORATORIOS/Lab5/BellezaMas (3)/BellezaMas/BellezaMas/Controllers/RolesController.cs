using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class RolesController : ControllerBase
{
    private readonly InventarioContext _ctx;

    public RolesController(InventarioContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RolDto>>> Obtener()
    {
        var roles = await _ctx.Roles
            .AsNoTracking()
            .OrderBy(r => r.NombreRol)
            .Select(r => new RolDto
            {
                Id = r.Id,
                NombreRol = r.NombreRol
            })
            .ToListAsync();

        return Ok(roles);
    }
}