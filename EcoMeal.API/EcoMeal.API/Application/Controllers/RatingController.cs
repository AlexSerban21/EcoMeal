using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcoMeal.API.Application;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    private int userID { get; set; }
    public RatingController(EcoMealDbContext context)
    {
        _context = context;
    }
    [HttpPut]
    [Route("AddRating")]
    public async Task<ActionResult> AddRating ([FromBody] RatingDTO rating)
    {
        GetCurrentUserId();
        var obj = await _context.Ratings.FirstOrDefaultAsync(r => r.UserId == userID && r.BusinessId == rating.BusinessId);
        if (obj == null)
        {
            _context.Ratings.Add(new Rating
            {
                UserId = userID,
                BusinessId = rating.BusinessId,
                Value = rating.Value,
            });
        }
        else
        {
            obj.Value = rating.Value;
        }
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpGet]
    [Route("GetRating/{businessId}")]
    public async Task<double> GetRating (int businessId)
    {
        GetCurrentUserId();
        var rating = await _context.Ratings
            .Where(r => r.BusinessId == businessId && r.UserId == userID)
            .FirstOrDefaultAsync();
        if (rating == null)
        {
            return 0;
        }
        return rating.Value;
    }
    private void GetCurrentUserId()
    {
        userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
