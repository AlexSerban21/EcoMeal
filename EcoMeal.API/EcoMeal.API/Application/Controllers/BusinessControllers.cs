using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Application;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    public BusinessController(EcoMealDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessDTO>>> GetAll()
    {
        var businessesDTOs = await _context.Businesses
            .Include(b => b.BusinessType)
            .Select(b => new BusinessDTO
            {
                Id = b.Id,
                Name = b.Name,
                Adress = b.Adress,
                Description = b.Description,
                Contact = b.Contact,
                BusinessTypeName = b.BusinessType.Name,
            }).ToListAsync();
        return Ok(businessesDTOs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessDetailsDTO>> GetOneById(int id)
    {
        var business = await _context.Businesses
            .Include(b => b.Packages)
            .ThenInclude(p => p.PackageType)
            .Select(b => new BusinessDetailsDTO
            {
                Id = b.Id,
                Name = b.Name,
                Adress = b.Adress,
                Description = b.Description,
                Contact = b.Contact,
                BusinessTypeName = b.BusinessType.Name,
            })
            .FirstOrDefaultAsync(b => b.Id == id);
        if (business is null)
        {
            return NotFound();
        }

        return Ok(business);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var business = await _context.Businesses.FindAsync(id);
        if (business is null)
        {
            return NotFound();
        }
        _context.Businesses.Remove(business);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    [Route("{id}/addPackage")]
    public async Task<IActionResult> AddPackageToBusiness(int id, [FromBody] PackageAddDTO package)
    {
        _context.Packages.Add(new Package
        {
            Name = package.Name,
            Description = package.Description,
            Price = package.Price,
            StartPickup = package.StartPickup,
            EndPickup = package.EndPickup,
            PackageTypeId = package.PackageTypeId,
            BusinessId = id,
            No_Package = 1,
        });
        await _context.SaveChangesAsync();
        return Created();
    }
}
