using EcoMeal.API.Entities;
using EcoMeal.API.Application.Models.Auth;
using EcoMeal.API.Application.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.API.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AuthController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Name,
            Contact = request.Contact
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        await _userManager.AddToRoleAsync(user, UserRoles.User);

        return Ok(new { Message = "User registered successfully" });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new UserMeResponse
        {
            Email = user.Email,
            Name = user.Name,
            Contact = user.Contact,
            Roles = roles
        });
    }

    [HttpGet("myName")]
    [Authorize]
    public async Task<ActionResult<string>> GetMyName()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();
        return Ok(user.Name);

    }
    [HttpPut("UpdateUser")]
    [Authorize]
    public async Task<ActionResult> UpdateUser ([FromBody] UpdateUserDTO request)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();
        if (request.Password != null)
        {
            var passwordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.Password);
            if (!passwordResult.Succeeded)
                return BadRequest(new { Errors = passwordResult.Errors.Select(e => e.Description) });
        }
        user.Name = request.Name;
        user.Contact = request.Contact;
        user.Email = request.Email;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        return Ok(new { Message = "User updated successfully" });
    }
    [HttpGet("GetProprieties")]
    [Authorize]
    public async Task<UserProprieties> GetProprieties ()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return new UserProprieties();
        return new UserProprieties
        {
            Email = user.Email,
            Name = user.Name,
            Contact = user.Contact
        };
    }
}
