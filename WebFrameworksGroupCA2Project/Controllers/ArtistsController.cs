using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.DTOs;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers
{

   // [Authorize]
    public class ArtistsController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly IWebHostEnvironment environment;

        public ArtistsController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            this.environment = environment;
        }

        // GET: Artists
        public async Task<IActionResult> Index(string artistGenre, string searchString)
        {
            if (_context.Artist == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from a in _context.Artist
                                            orderby a.Genre
                                            select a.Genre;
            var movies = from m in _context.Artist
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.ArtistName!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(artistGenre))
            {
                movies = movies.Where(x => x.Genre == artistGenre);
            }

            var artistGenreVM = new ArtistGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Artists = await movies.ToListAsync()
            };

            return View(artistGenreVM);
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .FirstOrDefaultAsync(m => m.Id == id);

            ArtistGetDTO artistDto = new ArtistGetDTO()
            {
                Id = artist.Id,
                ArtistName = artist.ArtistName,
                Genre = artist.Genre,
                BirthCountry = artist.BirthCountry,
                Overview = artist.Overview,
                ImageFileName = artist.ImageFileName,
            };

            if (artist == null)
            {
                return NotFound();
            }

            return View(artistDto);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {

            if (User.IsInRole("Admin"))
            {
                return View();

            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistPostDTO artistDto)
        {

            if (artistDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(artistDto);
            }

            if (artistDto.BirthCountry == null)
            {
                ModelState.AddModelError("", "Birth Country cant be empty");
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(artistDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                artistDto.ImageFile.CopyTo(stream);
            }

            Artist artist = new Artist()
            {
                ArtistName = artistDto.ArtistName,
                Genre = artistDto.Genre,
                BirthCountry = artistDto.BirthCountry,
                Overview = artistDto.Overview,
                ImageFileName = newFileName,
            };

            _context.Add(artist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.FindAsync(id);

            ArtistPutDTO artistDto = new ArtistPutDTO()
            {
                Id = artist.Id,
                ArtistName = artist.ArtistName,
                Genre = artist.Genre,
                BirthCountry = artist.BirthCountry,
                Overview = artist.Overview
            };

            if (artist == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                return View(artistDto);

            }

            return RedirectToAction(nameof(Index));

        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtistPutDTO artistDto)
        {

            if (artistDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(artistDto);
            }

            if (artistDto.BirthCountry == null)
            {
                ModelState.AddModelError("", "Birth Country cant be empty");
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(artistDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                artistDto.ImageFile.CopyTo(stream);
            }

            Artist artist = new()
            {
                Id = artistDto.Id,
                ArtistName = artistDto.ArtistName,
                Genre = artistDto.Genre,
                BirthCountry = artistDto.BirthCountry,
                Overview = artistDto.Overview,
                ImageFileName = newFileName,
            };




            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artistDto);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {

                return View(artist);

            }

            return RedirectToAction(nameof(Index));

        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            if (artist != null)
            {
                _context.Artist.Remove(artist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.Id == id);
        }
    }
}
