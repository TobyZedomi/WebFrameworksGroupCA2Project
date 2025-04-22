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
   // [Authorize]
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
        [HttpGet]
        public async Task<IActionResult> Index(double? minPrice, double? maxPrice, string searchString)
        {

            

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

            if (minPrice.HasValue)
            {
                vinyls = vinyls.Where(v => v.ListPrice >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                vinyls = vinyls.Where(v => v.ListPrice <= maxPrice);
            }
            

            var vinylVM = new VinylViewModel
            {
                ListPrice = new SelectList(await vinylQuery.Distinct().ToListAsync()),
                Vinyls = await vinyls.ToListAsync()
            };

            ViewBag.RequestVinyl = TempData["RequestVinyl"]; //reading temp data

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




        ////////////////////////////////////////////////////////////////// User can create a vinyl request 
        ///

        // GET: UserVinylRequests/Create
        [Authorize(Roles = "User, Admin")]
        public IActionResult CreateVinylRequest()
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
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreateVinylRequest(UserVinylRequestPostDTO userVinylRequestDto)
        {
            if (userVinylRequestDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(userVinylRequestDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(userVinylRequestDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                userVinylRequestDto.ImageFile.CopyTo(stream);
            }

            var userid = _userManager.GetUserId(HttpContext.User);

            UserVinylRequest userVinylRequest = new UserVinylRequest()
            {
                VinylName = userVinylRequestDto.VinylName,
                DateOfRelease = userVinylRequestDto.DateOfRelease,
                VinylInfo = userVinylRequestDto.VinylInfo,
                ListPrice = userVinylRequestDto.ListPrice,
                ImageFileName = newFileName,
                addedToStore = false,
                Status = "Pending",
                ArtistId = userVinylRequestDto.ArtistId,
                UserId = userid
            };

            _context.Add(userVinylRequest);
            await _context.SaveChangesAsync();

            TempData["RequestVinyl"] = $"{userVinylRequest.VinylName} vinyl has been requested to be added to the store";

            return RedirectToAction(nameof(UserVinylRequest));
        }



        // user can view there request 

        // GET: users request 
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UserVinylRequest()
        {

            var userid = _userManager.GetUserId(HttpContext.User);

            var vinylRequestByUser = _context.UserVinylRequest.Include(p => p.AppUser).Include(p => p.Artist).Where(x => x.UserId == userid);

            ViewBag.RequestVinyl = TempData["RequestVinyl"]; //reading temp data


            return View(await vinylRequestByUser.ToListAsync());
        }




        /// user can see details of there vinyl request 
        /// 

        // GET: UserVinylRequests/Details/5
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UserRequestDetails(int? id)
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


        // user can edit vinyl info only if its pending 


        // GET: Artists/Edit/5
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UserRequestEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVinylRequest = await _context.UserVinylRequest.FindAsync(id);

            if(userVinylRequest.addedToStore == true)
            {

                TempData["RequestVinyl"] = $" Vinyl Request Id {userVinylRequest.Id} with a name of {userVinylRequest.VinylName}: cant be added because its already added to the store";

                return RedirectToAction(nameof(UserVinylRequest));
            }


            UserVinylRequestPutDTO requestDto = new UserVinylRequestPutDTO()
            {
                Id = userVinylRequest.Id,
                VinylName = userVinylRequest.VinylName,
                DateOfRelease = userVinylRequest.DateOfRelease,
                VinylInfo = userVinylRequest.VinylInfo,
                ListPrice = userVinylRequest.ListPrice,
                ArtistId = userVinylRequest.ArtistId,

            };

            

            if (userVinylRequest == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User") || User.IsInRole("Admin"))
            {
                ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", userVinylRequest.ArtistId);
                TempData["RequestVinyl"] = $" Vinyl Request ID {userVinylRequest.Id} with a name of {userVinylRequest.VinylName}: has been edited";
                return View(requestDto);

            }

            return RedirectToAction(nameof(UserVinylRequest));

        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UserRequestEdit(int id, UserVinylRequestPutDTO userVinylRequestPutDTO)
        {

            if (userVinylRequestPutDTO.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(userVinylRequestPutDTO);
            }

           

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(userVinylRequestPutDTO.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                userVinylRequestPutDTO.ImageFile.CopyTo(stream);
            }

            var userid = _userManager.GetUserId(HttpContext.User);

            UserVinylRequest userVinylRequest = new()
            {
                Id = userVinylRequestPutDTO.Id,
                VinylName = userVinylRequestPutDTO.VinylName,
                DateOfRelease = userVinylRequestPutDTO.DateOfRelease,
                VinylInfo = userVinylRequestPutDTO.VinylInfo,
                ListPrice = userVinylRequestPutDTO.ListPrice,
                ImageFileName = newFileName,
                addedToStore = false,
                Status = "Pending",
                ArtistId = userVinylRequestPutDTO.ArtistId,
                UserId = userid

            };


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
                return RedirectToAction(nameof(UserVinylRequest));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "ArtistName", userVinylRequest.ArtistId);
            return View(userVinylRequestPutDTO);
        }



        // Vinyl Request 
        [Authorize(Roles = "User, Admin")]

        public async Task<IActionResult> UserRequestDelete(int? id)
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


            TempData["RequestVinyl"] = $" Vinyl Request ID {userVinylRequest.Id} with a name of {userVinylRequest.VinylName} : has been deleted";

            return View(userVinylRequest);
        }


        // POST: UserVinylRequests/Delete/5
        [HttpPost, ActionName("UserRequestDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UserRequestDelete(int id)
        {
            var userVinylRequest = await _context.UserVinylRequest.FindAsync(id);
            if (userVinylRequest != null)
            {
                _context.UserVinylRequest.Remove(userVinylRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserVinylRequest));
        }
        

        private bool UserVinylRequestExists(int id)
        {
            return _context.UserVinylRequest.Any(e => e.Id == id);
        }

    }
}
