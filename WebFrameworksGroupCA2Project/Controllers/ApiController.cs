using Microsoft.AspNetCore.Mvc;
using WebFrameworksGroupCA2Project.Data;

namespace WebFrameworksGroupCA2Project.Controllers;

[ApiController]
public class ApiController : ControllerBase // It's good practice to explicitly inherit from ControllerBase
{


    private readonly WebFrameworksGroupCA2ProjectContext _context;

    public ApiController(WebFrameworksGroupCA2ProjectContext context)
    {
        _context = context;
    }


    [HttpGet("vinyls/search")]
    public IActionResult SearchVinyls(string searchString, int pageNumber = 1, int pageSize = 10)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return BadRequest(); 
        }

        var query = _context.Vinyl
            .Where(v => v.VinylName.Contains(searchString));

        var totalItems = query.Count();
        var vinyls = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        

        var result = new
        {
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = vinyls
        };

        return Ok(result);
    }

    [HttpGet("test")] public IActionResult Test() => Ok("working");

}