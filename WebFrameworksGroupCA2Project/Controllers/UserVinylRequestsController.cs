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
    public class UserVinylRequestsController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;

        public UserVinylRequestsController(WebFrameworksGroupCA2ProjectContext context)
        {
            _context = context;
        }

        // GET: UserVinylRequests
        public async Task<IActionResult> Index()
        {
            var webFrameworksGroupCA2ProjectContext = _context.UserVinylRequest.Include(u => u.AppUser).Include(u => u.Artist);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }

        // GET: UserVinylRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVinylRequest = await _context.UserVinylRequest
                .Include(u => u.AppUser)
                .Include(u => u.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userVinylRequest == null)
            {
                return NotFound();
            }

            return View(userVinylRequest);
        }

        // GET: UserVinylRequests/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName");
            return View();
        }

        // POST: UserVinylRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VinylName,DateOfRelease,VinylInfo,ImageFileName,addedToStore,Status,ArtistId,UserId")] UserVinylRequest userVinylRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userVinylRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userVinylRequest.UserId);
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", userVinylRequest.ArtistId);
            return View(userVinylRequest);
        }

        // GET: UserVinylRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVinylRequest = await _context.UserVinylRequest.FindAsync(id);
            if (userVinylRequest == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userVinylRequest.UserId);
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", userVinylRequest.ArtistId);
            return View(userVinylRequest);
        }

        // POST: UserVinylRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VinylName,DateOfRelease,VinylInfo,ImageFileName,addedToStore,Status,ArtistId,UserId")] UserVinylRequest userVinylRequest)
        {
            if (id != userVinylRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userVinylRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserVinylRequestExists(userVinylRequest.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userVinylRequest.UserId);
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", userVinylRequest.ArtistId);
            return View(userVinylRequest);
        }

        // GET: UserVinylRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVinylRequest = await _context.UserVinylRequest
                .Include(u => u.AppUser)
                .Include(u => u.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userVinylRequest == null)
            {
                return NotFound();
            }

            return View(userVinylRequest);
        }

        // POST: UserVinylRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userVinylRequest = await _context.UserVinylRequest.FindAsync(id);
            if (userVinylRequest != null)
            {
                _context.UserVinylRequest.Remove(userVinylRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserVinylRequestExists(int id)
        {
            return _context.UserVinylRequest.Any(e => e.Id == id);
        }
    }
}
