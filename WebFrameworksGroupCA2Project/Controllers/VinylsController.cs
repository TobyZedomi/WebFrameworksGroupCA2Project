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

    [Authorize]
    public class VinylsController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment environment;

        public VinylsController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            this.environment = environment;
        }

        // GET: Vinyls
        public async Task<IActionResult> Index(double listPrice, string searchString)
        {

            if (_context.Vinyl == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<double> vinylQuery = from a in _context.Vinyl
                                            orderby a.ListPrice
                                            select a.ListPrice;
            var vinyls = from m in _context.Vinyl
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                vinyls = vinyls.Where(s => s.VinylName!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!listPrice.Equals(0))
            {
                vinyls = vinyls.Where(x => x.ListPrice == listPrice);
            }

            var vinylVM = new VinylViewModel
            {
                ListPrice = new SelectList(await vinylQuery.Distinct().ToListAsync()),
                Vinyls = await vinyls.ToListAsync()
            };

            return View(vinylVM);
        }

        // GET: Vinyls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinyl = await _context.Vinyl
                .Include(v => v.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinyl == null)
            {
                return NotFound();
            }

            return View(vinyl);
        }

        // GET: Vinyls/Create
        public IActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName");
                return View();

            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Vinyls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VinylPostDTO vinylDto)
        {
            if (vinylDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(vinylDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(vinylDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                vinylDto.ImageFile.CopyTo(stream);
            }

            Vinyl vinyl = new Vinyl()
            {
                VinylName = vinylDto.VinylName,
                DateOfRelease = vinylDto.DateOfRelease,
                ListPrice = vinylDto.ListPrice,
                VinylInfo = vinylDto.VinylInfo,
                ImageFileName = newFileName,
                ArtistId = vinylDto.ArtistId
            };

            _context.Add(vinyl);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Vinyls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinyl = await _context.Vinyl.FindAsync(id);


            VinylPutDTO vinylPutDto = new VinylPutDTO()
            {
                VinylName = vinyl.VinylName,
                DateOfRelease = vinyl.DateOfRelease,
                ListPrice = vinyl.ListPrice,
                VinylInfo = vinyl.VinylInfo,
                ArtistId = vinyl.ArtistId
            };

            if (vinyl == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", vinyl.ArtistId);
                return View(vinylPutDto);

            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Vinyls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VinylPutDTO vinylDto)
        {

            if (vinylDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(vinylDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(vinylDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                vinylDto.ImageFile.CopyTo(stream);
            }

            Vinyl vinyl = new Vinyl()
            {
                VinylName = vinylDto.VinylName,
                DateOfRelease = vinylDto.DateOfRelease,
                ListPrice = vinylDto.ListPrice,
                VinylInfo = vinylDto.VinylInfo,
                ImageFileName = newFileName,
                ArtistId = vinylDto.ArtistId
            };



            if (id != vinyl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vinyl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VinylExists(vinyl.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", vinyl.ArtistId);
            return View(vinylDto);
        }

        // GET: Vinyls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinyl = await _context.Vinyl
                .Include(v => v.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinyl == null)
            {
                return NotFound();
            }


            if (User.IsInRole("Admin"))
            {

                return View(vinyl);

            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Vinyls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vinyl = await _context.Vinyl.FindAsync(id);
            if (vinyl != null)
            {
                _context.Vinyl.Remove(vinyl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VinylExists(int id)
        {
            return _context.Vinyl.Any(e => e.Id == id);
        }
    }
}
