using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Application;

[ApiController]
[Route("api/[controller]")]
public class PackageController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    public PackageController(EcoMealDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("GetFromBusinessId/{id}")]
    public async Task<ActionResult<IEnumerable<PackageGetDTO>>> GetFromBusinessId(int id)
    {
        var Packages = await _context.Packages.Where(p => p.BusinessId == id).Select(p => new PackageGetDTO
        {
            Id = p.Id,
            Name = p.Name,
            PackageType = p.PackageType.Name,
            Description = p.Description,
            Price = p.Price,
            StartPickup = p.StartPickup,
            EndPickup = p.EndPickup,
            PackageTypeId = p.PackageTypeId,
            BusinessId = p.BusinessId
        }).ToListAsync();
        return Ok(Packages);
    }
    [HttpGet]
    [Route("GetOneById/{id}")]
    public async Task<ActionResult<PackageGetDTO>> GetOneById(int id)
    {
        var package = await _context.Packages
            .Select(b => new PackageGetDTO
            {
                Id = b.Id,
                Name = b.Name,
                PackageType = b.PackageType.Name,
                Description = b.Description,
                Price = b.Price,
                StartPickup = b.StartPickup,
                EndPickup = b.EndPickup,
                PackageTypeId = b.PackageTypeId,
                BusinessId = b.BusinessId
            })
            .FirstOrDefaultAsync(b => b.Id == id);
        if (package is null)
        {
            return NotFound();
        }

        return Ok(package);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var package = await _context.Packages.FindAsync(id);
        if (package is null)
        {
            return NotFound();
        }
        _context.Remove(package);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    [Route("AddToBusiness/{id}")]
    public async Task<IActionResult> AddToBusiness(int id, [FromBody] PackageAddDTO package)
    {
        _context.Packages.Add(new Package
        {
            Name = package.Name,
            Description = package.Description,
            Price = package.Price,
            StartPickup = package.StartPickup,
            EndPickup = package.EndPickup,
            PackageTypeId = package.PackageTypeId,
            BusinessId = id
        });
        await _context.SaveChangesAsync();
        return Created();
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PackageAddDTO package)
    {
        var PackageContext = await _context.Packages.FindAsync(id);
        if (PackageContext is null)
            return NotFound();

        PackageContext.Name = package.Name;
        PackageContext.Description = package.Description;
        PackageContext.Price = package.Price;
        PackageContext.StartPickup = package.StartPickup;
        PackageContext.EndPickup = package.EndPickup;
        PackageContext.PackageTypeId = package.PackageTypeId;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}