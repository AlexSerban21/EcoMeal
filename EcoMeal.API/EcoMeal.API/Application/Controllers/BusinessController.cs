using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Application;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public BusinessController(EcoMealDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<BusinessDTO>>> GetAll()
    {
        var businessesDTOs = await _context.Businesses
            .Select(b => new BusinessDTO
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Contact = b.Contact,
                BusinessTypeName = b.BusinessType.Name,
                BusinessTypeId = b.BusinessTypeId,
                Image = b.BusinessType.Image,
                CityId = b.CityId,
                CityName = b.City.Name,
                Rating = b.Ratings.Any() ? b.Ratings.Average(p => p.Value) : 0
            }).ToListAsync();
        return Ok(businessesDTOs);
    }

    [HttpGet("GetOneById/{id}")]
    [Authorize]
    public async Task<ActionResult<BusinessDTO>> GetOneById(int id)
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var business = await _context.Businesses
            .Include(b => b.Packages)
            .ThenInclude(p => p.PackageType)
            .Select(b => new BusinessDTO
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Contact = b.Contact,
                BusinessTypeName = b.BusinessType.Name,
                BusinessTypeId = b.BusinessTypeId,
                Image = b.BusinessType.Image,
                CityId = b.CityId,
                CityName = b.City.Name,
                Rating = b.Ratings.Any()? b.Ratings.Average(p => p.Value): 0
            })
            .FirstOrDefaultAsync(b => b.Id == id);
        if (business is null)
        {
            return NotFound();
        }

        return Ok(business);
    }

    [HttpDelete("Delete/{id}")]
   [Authorize]
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
    
    [HttpPost("AddBusiness")]
   [Authorize]
    public async Task<IActionResult> AddBusiness([FromBody] BusinessAddDTO business)
    {
        _context.Businesses.Add(new Business
        {
            Name = business.Name,
            Description = business.Description,
            Contact = business.Contact,
            BusinessTypeId = business.BusinessTypeId,
            CityId = business.CityId
        });
        await _context.SaveChangesAsync();
        return Created();
    }
    
    [HttpPut("UpdateBusiness/{id}")]
   [Authorize]
    public async Task<IActionResult> UpdateBusiness(int id, [FromBody] BusinessAddDTO business)
    {
        var businessContext = await _context.Businesses.FindAsync(id);
        if (businessContext is null)
            return NotFound();

        businessContext.Name = business.Name;
        businessContext.Description = business.Description;
        businessContext.Contact = business.Contact;
        businessContext.BusinessTypeId = business.BusinessTypeId;
        businessContext.CityId = business.CityId;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
