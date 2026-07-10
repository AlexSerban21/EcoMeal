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

    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessDTO>> GetOneById(int id)
    {
        Console.WriteLine("333");
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
            Console.WriteLine("444");
            return NotFound();
        }

        return Ok(business);
    }

    [HttpDelete("deleteBusiness/{id}")]
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
    [HttpDelete("package/{id}")]
    public async Task<IActionResult> DeletePackage(int id)
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
            BusinessId = id
        });
        await _context.SaveChangesAsync();
        return Created();
    }

    [HttpPost]
    [Route("addBusiness")]
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

    [HttpGet]
    [Route("packageTypes")]
    public async Task<ActionResult<IEnumerable<PackageTypeDTO>>> GetPackageTypes()
    {
        var PackageTypesDTOs = await _context.PackageTypes.Select (p => new PackageTypeDTO
        {
            Id = p.Id,
            Name = p.Name,
        }).ToListAsync ();
        return Ok(PackageTypesDTOs);
    }

    [HttpGet]
    [Route("{id}/packages")]

    public async Task<ActionResult<IEnumerable<PackageGetDTO>>> GetPackagesFromBusinessId(int id)
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
            PackageTypeId = p.PackageTypeId
        }).ToListAsync();
        return Ok(Packages);
    }
    [HttpGet]
    [Route("getPackage/{id}")]
    public async Task<ActionResult<PackageGetDTO>> GetPackageById (int id)
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
                PackageTypeId = b.PackageTypeId
            })
            .FirstOrDefaultAsync(b => b.Id == id);
        if (package is null)
        {
            return NotFound();
        }

        return Ok(package);
    }

    [HttpGet]
    [Route("businessTypes")]
    public async Task<ActionResult<IEnumerable<BusinessTypeDTO>>> GetBusinessTypes()
    {
        var BusinessTypesDTOs = await _context.BusinessTypes.Select(p => new BusinessTypeDTO
        {
            Id = p.Id,
            Name = p.Name,
        }).ToListAsync();
        return Ok(BusinessTypesDTOs);
    }
    [HttpPut]
    [Route("{id}/updateBusiness")]
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

    [HttpPut]
    [Route("{id}/updatePackage")]
    public async Task<IActionResult> UpdatePackage(int id, [FromBody] PackageAddDTO package)
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
        PackageContext.BusinessId = id;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
