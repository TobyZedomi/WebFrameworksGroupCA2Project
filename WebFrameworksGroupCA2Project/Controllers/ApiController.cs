using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Data;
using System.Linq;

namespace WebFrameworksGroupCA2Project.Controllers;

[ApiController]
[Route("api")] 
public class ApiController : ControllerBase
{
    private readonly WebFrameworksGroupCA2ProjectContext _context;

    public ApiController(WebFrameworksGroupCA2ProjectContext context)
    {
        _context = context;
    }

    [HttpGet("vinyls/search")]
    public IActionResult SearchVinyls(string? searchString, int pageNumber = 1, int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return BadRequest("Search string must not be empty.");
        }

        if (pageNumber <= 0)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return BadRequest("Page size must be greater than 0.");
        }

        var query = _context.Vinyl
            .Where(v => v.VinylName!.Contains(searchString));

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

    [HttpGet("vinyls/top-sold")]
    public IActionResult GetTopSoldVinyls(int topN = 5)
    {
        if (topN <= 0)
        {
            return BadRequest("topN must be greater than 0.");
        }

        var topSoldVinyls = _context.Vinyl
            .OrderByDescending(v => v.OrderItems!.Count)
            .Include(v => v.Artist)
            .Take(topN)
            .Select(v => new
            {
                v.Id,
                v.VinylName,
                v.ListPrice,
                v.DateOfRelease,
                v.Artist!.ArtistName,
                TotalSold = v.OrderItems!.Count
            })
            .ToList();

        return Ok(topSoldVinyls);
    }

    [HttpGet("songs/search")]
    public IActionResult SearchSongs(string? searchString, int pageNumber = 1, int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return BadRequest("Search string must not be empty.");
        }

        if (pageNumber <= 0)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return BadRequest("Page size must be greater than 0.");
        }

        var query = _context.Song
            .Where(s => s.SongName!.Contains(searchString));

        var totalItems = query.Count();
        var songs = query
            .Include(s => s.Artist)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new
            {
                s.Id,
                s.SongName,
                s.DateOfRelease,
                s.SongDescription,
                s.Artist
            })
            .ToList();

        var result = new
        {
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = songs
        };

        return Ok(result);
    }

    [HttpGet("artists/search")]
    public IActionResult SearchArtists(string? searchString, int pageNumber = 1, int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return BadRequest("Search string must not be empty.");
        }

        if (pageNumber <= 0)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return BadRequest("Page size must be greater than 0.");
        }

        var query = _context.Artist
            .Where(a => a.ArtistName!.Contains(searchString));

        var totalItems = query.Count();
        var artists = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new
            {
                a.Id,
                a.ArtistName,
                a.Genre,
                a.BirthCountry
            })
            .ToList();

        var result = new
        {
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = artists
        };

        return Ok(result);
    }

    [HttpGet("playlists/search")]
    public IActionResult SearchPlaylists(string? searchString, int pageNumber = 1, int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return BadRequest("Search string must not be empty.");
        }

        if (pageNumber <= 0)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return BadRequest("Page size must be greater than 0.");
        }

        var query = _context.Playlist
            .Where(p => p.PlaylistName!.Contains(searchString));

        var totalItems = query.Count();
        var playlists = query
            .Include(p => p.AppUser)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new
            {
                p.Id,
                p.PlaylistName,
                p.StatusPrivate,
                Creator = p.AppUser!.UserName 
            })
            .ToList();

        var result = new
        {
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = playlists
        };

        return Ok(result);
    }
}