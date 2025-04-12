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
    public class CartInfoesController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;

        public CartInfoesController(WebFrameworksGroupCA2ProjectContext context)
        {
            _context = context;
        }

        // GET: CartInfoes
        public async Task<IActionResult> Index()
        {
            var webFrameworksGroupCA2ProjectContext = _context.CartInfo.Include(c => c.ShoppingCart).Include(c => c.Vinyl);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }

        // GET: CartInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartInfo = await _context.CartInfo
                .Include(c => c.ShoppingCart)
                .Include(c => c.Vinyl)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartInfo == null)
            {
                return NotFound();
            }

            return View(cartInfo);
        }

        // GET: CartInfoes/Create
        public IActionResult Create()
        {
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id");
            ViewData["VinylId"] = new SelectList(_context.Vinyl, "Id", "VinylName");
            return View();
        }

        // POST: CartInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,UnitPrice,VinylId,ShoppingCartId")] CartInfo cartInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id", cartInfo.ShoppingCartId);
            ViewData["VinylId"] = new SelectList(_context.Vinyl, "Id", "VinylName", cartInfo.VinylId);
            return View(cartInfo);
        }

        // GET: CartInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartInfo = await _context.CartInfo.FindAsync(id);
            if (cartInfo == null)
            {
                return NotFound();
            }
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id", cartInfo.ShoppingCartId);
            ViewData["VinylId"] = new SelectList(_context.Vinyl, "Id", "VinylName", cartInfo.VinylId);
            return View(cartInfo);
        }

        // POST: CartInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,UnitPrice,VinylId,ShoppingCartId")] CartInfo cartInfo)
        {
            if (id != cartInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartInfoExists(cartInfo.Id))
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
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id", cartInfo.ShoppingCartId);
            ViewData["VinylId"] = new SelectList(_context.Vinyl, "Id", "VinylName", cartInfo.VinylId);
            return View(cartInfo);
        }

        // GET: CartInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartInfo = await _context.CartInfo
                .Include(c => c.ShoppingCart)
                .Include(c => c.Vinyl)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartInfo == null)
            {
                return NotFound();
            }

            return View(cartInfo);
        }

        // POST: CartInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartInfo = await _context.CartInfo.FindAsync(id);
            if (cartInfo != null)
            {
                _context.CartInfo.Remove(cartInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartInfoExists(int id)
        {
            return _context.CartInfo.Any(e => e.Id == id);
        }
    }
}
