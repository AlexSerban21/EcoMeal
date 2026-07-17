using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcoMeal.API.Application.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FavouriteBusinessController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    private int userId;
    public FavouriteBusinessController (EcoMealDbContext context)
    {
        _context = context;
    }
    [HttpGet("Check/{businessId}")]
    public Task<bool> Check (int businessId)
    {
        GetCurrentUserId();
        return _context.FavouriteBusinesses.AnyAsync(fb => fb.UserId == userId && fb.BusinessId == businessId);
    }
    [HttpPost("Add/{businessId}")]
    public async Task<ActionResult> Add(int businessId)
    {
        GetCurrentUserId();
        var favouriteBusiness = new FavouriteBusiness
        {
            UserId = userId,
            BusinessId = businessId
        };
        _context.FavouriteBusinesses.Add(favouriteBusiness);
        await _context.SaveChangesAsync();
        return Created();
    }
    [HttpDelete("Delete/{businessId}")]
    public async Task<ActionResult> Delete(int businessId)
    {
        GetCurrentUserId();
        var favouriteBusiness = await _context.FavouriteBusinesses
            .FirstOrDefaultAsync(fb => fb.UserId == userId && fb.BusinessId == businessId);
        if (favouriteBusiness == null)
        {
            return NotFound();
        }
        _context.FavouriteBusinesses.Remove(favouriteBusiness);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpGet("Get")]
    public async Task<ActionResult<List<FavouriteBusiness>>> Get()
    {
        GetCurrentUserId();
        var favouriteBusinesses = await _context.FavouriteBusinesses
            .Where(fb => fb.UserId == userId)
            .Select (fb => new BusinessDTO
            {
                Id = fb.Business.Id,
                Name = fb.Business.Name,
                Description = fb.Business.Description,
                Contact = fb.Business.Contact,
                BusinessTypeName = fb.Business.BusinessType.Name,
                BusinessTypeId = fb.Business.BusinessTypeId,
                CityId = fb.Business.CityId,
                Image = fb.Business.BusinessType.Image,
                CityName = fb.Business.City.Name
                //Rating = fb.Business.Rating
            }).ToListAsync();
        return Ok(favouriteBusinesses);
    }
    private void GetCurrentUserId()
    {
        userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
