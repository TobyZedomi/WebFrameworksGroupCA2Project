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
    public class RatingsController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        private readonly IWebHostEnvironment environment;

        private readonly WebFrameworksGroupCA2ProjectContext _context;

        public RatingsController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            this.environment = environment;
        }


        // GET: RatingsUser
        public async Task<IActionResult> Index()
        {

            var userid = _userManager.GetUserId(HttpContext.User);

            var webFrameworksGroupCA2ProjectContext = _context.Rating.Include(r => r.Song).Include(r => r.User).Where(x => x.UserId == userid);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }

        // GET: RatingsUser
        public async Task<IActionResult> ViewAllRatings()
        {


            var webFrameworksGroupCA2ProjectContext = _context.Rating.Include(r => r.Song).Include(r => r.User);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Song)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RatingPostDTO ratingDTO)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            Rating rating = new Rating()
            {
                UserRating = ratingDTO.UserRating,
                Comment = ratingDTO.Comment,
                UserId = userId,
                SongId = ratingDTO.SongId

            };
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName", rating.SongId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rating.UserId);
            return View(ratingDTO);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName", rating.SongId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rating.UserId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,RatingPostDTO ratingDTO)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            Rating rating = new Rating()
            {
                UserRating = ratingDTO.UserRating,
                Comment = ratingDTO.Comment,
                UserId = userId,
                SongId = ratingDTO.SongId

            };
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
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
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName", rating.SongId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rating.UserId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Song)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating != null)
            {
                _context.Rating.Remove(rating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
    }
}
