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
    [HttpPost("placeOrder")]
    public async Task<ActionResult> PlaceOrder([FromBody] OrderCreateDTO request)
    {
        var userId = GetCurrentUserId();

        var package = await _context.Packages.FirstOrDefaultAsync(p => p.Id == request.PackageId);
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
        return Created();
    }
    [HttpGet("myOrders")]
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
                          BusinessName = o.Package.Business.Name,
                          PackageName = o.Package.Name
                      }).ToListAsync();
        return Ok(orders);

    }
    [HttpGet("WaitingOrders")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<OrderGetDTO>>> GetWaitingOrders()
    {
        var orders = await _context.Orders
                     .Where(o => o.Status == "Plasata")
                     .OrderByDescending(o => o.Date)
                     .Select(o => new OrderGetDTO
                     {
                         Id = o.Id,
                         Date = o.Date,
                         Status = o.Status,
                         Price = o.Package.Price,
                         BusinessId = o.Package.BusinessId,
                         BusinessName = o.Package.Business.Name,
                         PackageName = o.Package.Name
                     }).ToListAsync();
        return Ok(orders);
    }
    [HttpPut("ApproveOrder")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ApproveOrder([FromBody] int OrderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
        if (order is null)
        {
            return NotFound();
        }
        order.Status = "Aprobata";
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("CancelOrder/{OrderId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CancelOrder(int OrderId)
    {
        _context.Orders.RemoveRange(_context.Orders.Where(o => o.Id == OrderId));
        await _context.SaveChangesAsync();
        return Ok();
    }

    private int GetCurrentUserId ()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(userIdValue!);
    }
}
