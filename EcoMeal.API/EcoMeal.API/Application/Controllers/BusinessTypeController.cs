using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Application;

[ApiController]
[Route("api/[controller]")]

public class BusinessTypeController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    public BusinessTypeController(EcoMealDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<BusinessTypeDTO>>> GetAll()
    {
        var BusinessTypesDTOs = await _context.BusinessTypes.Select(p => new BusinessTypeDTO
        {
            Id = p.Id,
            Name = p.Name,
        }).ToListAsync();
        return Ok(BusinessTypesDTOs);
    }
}
