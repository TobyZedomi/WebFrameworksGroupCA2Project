using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers
{
    public class PurchaseController : Controller
    {

        private readonly WebFrameworksGroupCA2ProjectContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly IWebHostEnvironment environment;

        public PurchaseController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            this.environment = environment;
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> ViewPurchase()
        {

            var userid = _userManager.GetUserId(HttpContext.User);

            var purchaseByUser = _context.Purchases
                .Include(p => p.AppUser)
                .Include(p => p.OrderItems)
                .ThenInclude(v => v.Vinyl)
                .Where(x => x.UserId == userid);

            return View(await purchaseByUser.ToListAsync());
        }
        

      




    }
}
