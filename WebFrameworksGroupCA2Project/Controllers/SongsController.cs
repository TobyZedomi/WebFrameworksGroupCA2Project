using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.DTOs;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers
{

    [Authorize]
    public class SongsController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment environment;

        public SongsController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            this.environment = environment;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Song == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }


            var songs = from m in _context.Song
                        select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.SongName!.ToUpper().Contains(searchString.ToUpper()));
            }



            var songArtistNameVM = new SongArtistNameViewModel
            {
                Songs = await songs.ToListAsync()
            };

            return View(songArtistNameVM);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);

            SongGetDTO songDto = new SongGetDTO()
            {
                Id = song.Id,
                SongName = song.SongName,
                DateOfRelease = song.DateOfRelease,
                SongDescription = song.SongDescription,
                Artist = song.Artist,

            };

            if (song == null)
            {
                return NotFound();
            }

            return View(songDto);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {

            if (User.IsInRole("Admin"))
            {
                ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongPostDTO songDto)
        {
            if (songDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(songDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(songDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                songDto.ImageFile.CopyTo(stream);
            }

            Song song = new Song()
            {
                SongName = songDto.SongName,
                SongDescription = songDto.SongDescription,
                ImageFileName = newFileName,
                ArtistId = songDto.ArtistId
            };

            _context.Add(song);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);

            SongPutDTO songPutDto = new SongPutDTO()
            {
                Id = song.Id,
                SongName = song.SongName,
                SongDescription = song.SongDescription,
                DateOfRelease = song.DateOfRelease,
                ArtistId = song.ArtistId,
            };

            if (song == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", song.ArtistId);
                return View(songPutDto);

            }
            return RedirectToAction(nameof(Index));

        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SongPutDTO songDto)
        {

            if (songDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(songDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(songDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                songDto.ImageFile.CopyTo(stream);
            }

            Song song = new Song()
            {
                Id = songDto.Id,
                SongName = songDto.SongName,
                SongDescription = songDto.SongDescription,
                DateOfRelease = songDto.DateOfRelease,
                ImageFileName = newFileName,
                ArtistId = songDto.ArtistId
            };


            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", song.ArtistId);
            return View(songDto);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {

                return View(song);

            }

            return RedirectToAction(nameof(Index));

        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song != null)
            {
                _context.Song.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }
    }
}
