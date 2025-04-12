using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers
{

    [Authorize]
    public class VinylsController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;

        public VinylsController(WebFrameworksGroupCA2ProjectContext context)
        {
            _context = context;
        }

        // GET: Vinyls
        public async Task<IActionResult> Index()
        {
            var webFrameworksGroupCA2ProjectContext = _context.Vinyl.Include(v => v.Artist);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName");
            return View();
        }

        // POST: Vinyls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VinylName,DateOfRelease,ListPrice,VinylInfo,ImageFileName,ArtistId")] Vinyl vinyl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vinyl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", vinyl.ArtistId);
            return View(vinyl);
        }

        // GET: Vinyls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinyl = await _context.Vinyl.FindAsync(id);
            if (vinyl == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", vinyl.ArtistId);
            return View(vinyl);
        }

        // POST: Vinyls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VinylName,DateOfRelease,ListPrice,VinylInfo,ImageFileName,ArtistId")] Vinyl vinyl)
        {
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
            return View(vinyl);
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

            return View(vinyl);
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
