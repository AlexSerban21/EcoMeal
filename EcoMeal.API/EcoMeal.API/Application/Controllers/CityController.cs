using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Application;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    public CityController(EcoMealDbContext context)
    {
        _context = context;
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<CityDTO>>> GetAll ()
    {
        var cities = await _context.Cities
            .Select(c => new CityDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
        return Ok(cities);
    }
}
