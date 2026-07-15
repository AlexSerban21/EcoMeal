using EcoMeal.API.Application.Models;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcoMeal.API.Application.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class OrderController : ControllerBase 
{
    private readonly EcoMealDbContext _context;

    public OrderController(EcoMealDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<ActionResult<OrderGetDTO>> PlaceOrder([FromBody] OrderCreateDTO request)
    {
        var userId = GetCurrentUserId();

        var package = await _context.Packages.Include(p => p.Business)
                                    .Include(package => package.Orders)
                                    .FirstOrDefaultAsync(p => p.Id == request.PackageId);
        if (package.Orders.Any())
        {
            return BadRequest("Pachetul nu mai e disponibil!");
        }
        var order = new Order
        {
            UserId = userId,
            PackageId = package.Id,
            Status = "Plasata",
            Date = DateTime.Now,
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Ok(new OrderGetDTO
        {
            Id = order.Id,
            Date = order.Date,
            Status = order.Status,
            PackageName = package.Name,
            Price = package.Price,
            BusinessId = package.BusinessId,
            BusinessName = package.Business.Name,
            UserName = order.User.Name,
            UserContact = order.User.Contact
        });
    }
    [HttpGet("my")]
    [Authorize]
    public async Task<ActionResult<List<OrderGetDTO>>> GetMyOrders ()
    {
        var userId = GetCurrentUserId();
        var orders = await _context.Orders
                     .Where(o => o.UserId == userId)
                     .OrderByDescending(o => o.Date)
                     .Select(o => new OrderGetDTO
                      {
                          Id = o.Id,
                          Date = o.Date,
                          Status = o.Status,
                          Price = o.Package.Price,
                          BusinessId = o.Package.BusinessId,
                          BusinessName = o.Package.Name,
                          PackageName = o.Package.Name
                      }).ToListAsync();
        return Ok(orders);

    }
    private int GetCurrentUserId ()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(userIdValue!);
    }
}
