using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers
{
    public class VinylStoresController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;

        public VinylStoresController(WebFrameworksGroupCA2ProjectContext context)
        {
            _context = context;
        }

        // GET: VinylStores
        public async Task<IActionResult> Index()
        {
            var webFrameworksGroupCA2ProjectContext = _context.VinylStore.Include(v => v.Artist);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }

        // GET: VinylStores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinylStore = await _context.VinylStore
                .Include(v => v.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinylStore == null)
            {
                return NotFound();
            }

            return View(vinylStore);
        }

        // GET: VinylStores/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName");
            return View();
        }

        // POST: VinylStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VinylName,DateOfRelease,Quantity,ListPrice,VinylInfo,ImageFileName,ArtistId")] VinylStore vinylStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vinylStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", vinylStore.ArtistId);
            return View(vinylStore);
        }

        // GET: VinylStores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinylStore = await _context.VinylStore.FindAsync(id);
            if (vinylStore == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", vinylStore.ArtistId);
            return View(vinylStore);
        }

        // POST: VinylStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VinylName,DateOfRelease,Quantity,ListPrice,VinylInfo,ImageFileName,ArtistId")] VinylStore vinylStore)
        {
            if (id != vinylStore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vinylStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VinylStoreExists(vinylStore.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", vinylStore.ArtistId);
            return View(vinylStore);
        }

        // GET: VinylStores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinylStore = await _context.VinylStore
                .Include(v => v.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinylStore == null)
            {
                return NotFound();
            }

            return View(vinylStore);
        }

        // POST: VinylStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vinylStore = await _context.VinylStore.FindAsync(id);
            if (vinylStore != null)
            {
                _context.VinylStore.Remove(vinylStore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VinylStoreExists(int id)
        {
            return _context.VinylStore.Any(e => e.Id == id);
        }
    }
}
