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
    [HttpPost]
    [Route("AddRating")]
    public async Task<ActionResult> AddRating ([FromBody] RatingDTO rating)
    {
        GetCurrentUserId();
        _context.Ratings.Add(new Rating
        {
            UserId = userID,
            BusinessId = rating.BusinessId,
            Value = rating.Value,
        });
        await _context.SaveChangesAsync();
        return Created();
    }
    private void GetCurrentUserId()
    {
        userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
