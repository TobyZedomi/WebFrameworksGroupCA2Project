using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        
        private readonly WebFrameworksGroupCA2ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment environment;


        public AdminController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            this.environment = environment;
        }


        public async Task<IActionResult> Index()
        {

            var vinylRequestByUser = _context.UserVinylRequest.Include(p => p.AppUser).Include(p => p.Artist).Where(x => x.addedToStore == false);

            return View(await vinylRequestByUser.ToListAsync());
        }


        // accept user request

        public IActionResult AcceptRequest(int? id)
        {
            var vinylToAdd = _context.UserVinylRequest.Find(id);


            Vinyl vinyl = new Vinyl()
            {
                VinylName = vinylToAdd.VinylName,
                DateOfRelease = vinylToAdd.DateOfRelease,
                ListPrice = 30,
                VinylInfo = vinylToAdd.VinylInfo,
                ImageFileName = vinylToAdd.ImageFileName,
                ArtistId = vinylToAdd.ArtistId
            };
            _context.Vinyl.Add(vinyl);

   

            // save chaneges to the database

            _context.SaveChanges();

            
            UserVinylRequest userVinylRequest = new UserVinylRequest()
            {
               
                VinylName = vinylToAdd.VinylName,
                DateOfRelease =vinylToAdd.DateOfRelease,
                VinylInfo = vinylToAdd.VinylInfo,
                ImageFileName = vinylToAdd.ImageFileName,
                addedToStore = true,
                Status = "ADDED TO STORE",
                ArtistId = vinylToAdd.ArtistId,
                UserId = vinylToAdd.UserId
            };

            _context.UserVinylRequest.Add(userVinylRequest);
            _context.UserVinylRequest.Remove(vinylToAdd);

            _context.SaveChanges();



            /// writing temp data

            TempData["RequestVinyl"] = $"{vinylToAdd.VinylName} vinyl added to store";


            return RedirectToAction("Index", "Vinyls");
        }
    }

    }

