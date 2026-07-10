using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Application;

[ApiController]
[Route("api/[controller]")]

public class PackageTypeController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    public PackageTypeController(EcoMealDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<PackageTypeDTO>>> GetAll()
    {
        var PackageTypesDTOs = await _context.PackageTypes.Select(p => new PackageTypeDTO
        {
            Id = p.Id,
            Name = p.Name,
        }).ToListAsync();
        return Ok(PackageTypesDTOs);
    }
}
