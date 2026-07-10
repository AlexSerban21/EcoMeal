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
                BusinessTypeId = b.BusinessTypeId
            }).ToListAsync();
        return Ok(businessesDTOs);
    }
    [HttpGet("GetOneById/{id}")]
    public async Task<ActionResult<BusinessDTO>> GetOneById(int id)
    {
        var business = await _context.Businesses
            .Include(b => b.Packages)
            .ThenInclude(p => p.PackageType)
            .Select(b => new BusinessDTO
            {
                Id = b.Id,
                Name = b.Name,
                Adress = b.Adress,
                Description = b.Description,
                Contact = b.Contact,
                BusinessTypeName = b.BusinessType.Name,
                BusinessTypeId = b.BusinessTypeId
            })
            .FirstOrDefaultAsync(b => b.Id == id);
        if (business is null)
        {
            return NotFound();
        }

        return Ok(business);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var business = await _context.Businesses.FindAsync(id);
        if (business is null)
        {
            return NotFound();
        }
        _context.Packages.RemoveRange(_context.Packages.Where(p => p.BusinessId == id));
        _context.Businesses.Remove(business);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBusiness([FromBody] BusinessAddDTO business)
    {
        _context.Businesses.Add(new Business
        {
            Name = business.Name,
            Adress = business.Adress,
            Description = business.Description,
            Contact = business.Contact,
            BusinessTypeId = business.BusinessTypeId
        });
        await _context.SaveChangesAsync();
        return Created();
    }
    
    [HttpPut]
    [Route("UpdateBusiness/{id}")]
    public async Task<IActionResult> UpdateBusiness(int id, [FromBody] BusinessAddDTO business)
    {
        var businessContext = await _context.Businesses.FindAsync(id);
        if (businessContext is null)
            return NotFound();

        businessContext.Name = business.Name;
        businessContext.Adress = business.Adress;
        businessContext.Description = business.Description;
        businessContext.Contact = business.Contact;
        businessContext.BusinessTypeId = business.BusinessTypeId;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
