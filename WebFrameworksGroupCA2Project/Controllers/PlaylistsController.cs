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
    public class PlaylistsController : Controller
    {
        private readonly WebFrameworksGroupCA2ProjectContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly IWebHostEnvironment environment;

        public PlaylistsController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            this.environment = environment;
        }

        // GET: PlaylistByUser
        public async Task<IActionResult> Index()
        {

            var userid = _userManager.GetUserId(HttpContext.User);

            var playlistByUser = _context.Playlist.Include(p => p.AppUser).Where(x => x.UserId == userid);

            return View(await playlistByUser.ToListAsync());
        }



        // GET: Playlists
        public async Task<IActionResult> GetAllPlaylistInTheSystem()
        {

            var userid = _userManager.GetUserId(HttpContext.User);

            var webFrameworksGroupCA2ProjectContext = _context.Playlist.Include(p => p.AppUser).Where(x => x.StatusPrivate == false);


            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userid = _userManager.GetUserId(HttpContext.User);

            var playlist = await _context.Playlist
                .Include(p => p.AppUser)
                .Include(p => p.PlaylistSongs)
                    .ThenInclude( p => p.Song)
                        .ThenInclude(p => p.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (playlist == null)
            {
                return NotFound();
            }

            PlaylistGetDTO playlistDto = new PlaylistGetDTO()
            {
                Id = playlist.Id,
                PlaylistName = playlist.PlaylistName,
                StatusPrivate = playlist.StatusPrivate,
                ImageFileName = playlist.ImageFileName,
                PlaylistSongs = playlist.PlaylistSongs
            };

            
            return View(playlistDto);
        }

        // GET: Playlists/Create

        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Create(PlaylistPostDTO playlistDto)
        {
            if (playlistDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(playlistDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(playlistDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                playlistDto.ImageFile.CopyTo(stream);
            }

            var userId = _userManager.GetUserId(HttpContext.User);

            Playlist playlist = new Playlist()
            {
                PlaylistName = playlistDto.PlaylistName,
                StatusPrivate = playlistDto.StatusPrivate,
                ImageFileName = newFileName,
                UserId = userId
            };

            _context.Add(playlist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // add song to particular playlist 


        // GET: PlaylistSongs/CreatePlaylistSong
        [Authorize(Roles = "User, Admin")]
        public IActionResult CreatePlaylistSong()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "PlaylistName");
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName");
            return View();
        }

        // method to add song to playlist

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlaylistSong(PlaylistSong playlistSong, int? id)
        {


            if (ModelState.IsValid)
            {

                var playlist = await _context.Playlist.FindAsync(id);

                playlistSong = new PlaylistSong()
                {
                    PlaylistId = playlist.Id,
                    SongId = playlistSong.SongId,
                };


                _context.Add(playlistSong);
                await _context.SaveChangesAsync();

                // find a way to redirect this to the get all Songs In Playlist for private playlist

                ViewBag.Message = "Song was added to your playlist.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "PlaylistName", playlistSong.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName", playlistSong.SongId);
            return View(playlistSong);
        }



        // get all songs in the playlist for private playlist

        // GET: Playlists
        public async Task<IActionResult> GetAllSongsInPlaylist(int? id)
        {

            var playlist = await _context.Playlist.FindAsync(id);


            var webFrameworksGroupCA2ProjectContext = _context.PlaylistSong.Include(p => p.Playlist).Include(p => p.Song).Where(p1 => p1.PlaylistId == playlist.Id);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }


        // get all songs in the playlist for public playlist

        // GET: All songsin Playlist
        public async Task<IActionResult> GetAllSongsInPlaylist2(int? id)
        {

            var playlist = await _context.Playlist.FindAsync(id);


            var webFrameworksGroupCA2ProjectContext = _context.PlaylistSong.Include(p => p.Playlist).Include(p => p.Song).Where(p1 => p1.PlaylistId == playlist.Id);
            return View(await webFrameworksGroupCA2ProjectContext.ToListAsync());
        }


        // GET: PlaylistSongs/Edit/5
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> EditPlaylistSong(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSong.FindAsync(id);
            if (playlistSong == null)
            {
                return NotFound();
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "PlaylistName", playlistSong.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName", playlistSong.SongId);
            return View(playlistSong);
        }


        // POST: PlaylistSongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> EditPlaylistSong(int id, [Bind("Id,PlaylistId,SongId")] PlaylistSong playlistSong)
        {
            if (id != playlistSong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlistSong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistSongExists(playlistSong.Id))
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
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "PlaylistName", playlistSong.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "SongName", playlistSong.SongId);
            return View(playlistSong);
        }

        /*
         * DELETE PLAYLIST SONG METHODS
         */

        // GET: PlaylistSongs/Delete/5
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeletePlaylistSong(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSong
                .Include(p => p.Playlist)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            return View(playlistSong);
        }

        // POST: PlaylistSongs/Delete/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost, ActionName("DeletePlaylistSong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlaylistSongConfirmed(int id)
        {
            var playlistSong = await _context.PlaylistSong.FindAsync(id);
            if (playlistSong != null)
            {
                _context.PlaylistSong.Remove(playlistSong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: PlaylistSongs/Details/5
        public async Task<IActionResult> DetailsPlaylistSong(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSong
                .Include(p => p.Playlist)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            return View(playlistSong);
        }


        // GET: Playlists/Edit/5
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlist.FindAsync(id);

            PlaylistPutDTO playlistPutDto = new PlaylistPutDTO()
            {
                Id = playlist.Id,
                PlaylistName = playlist.PlaylistName,
                StatusPrivate = playlist.StatusPrivate
            };

            var userid = _userManager.GetUserId(HttpContext.User);

            if (playlist == null)
            {
                return NotFound();
            }

            if (userid == playlist.UserId)
            {

                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playlist.UserId == userid);
                return View(playlistPutDto);

            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Edit(int id, PlaylistPutDTO playlistDto)
        {


            if (playlistDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required");
            }

            if (!ModelState.IsValid)
            {
                return View(playlistDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(playlistDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                playlistDto.ImageFile.CopyTo(stream);
            }

            var userid = _userManager.GetUserId(HttpContext.User);

            Playlist playlist = new Playlist()
            {
                Id = playlistDto.Id,
                PlaylistName = playlistDto.PlaylistName,
                StatusPrivate = playlistDto.StatusPrivate,
                ImageFileName = newFileName,
                UserId = userid
            };

            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playlist.UserId);
            return View(playlistDto);
        }

        // GET: Playlists/Delete/5
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userid = _userManager.GetUserId(HttpContext.User);

            var playlist = await _context.Playlist
                .Include(p => p.AppUser).Where(x => x.UserId == userid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlist = await _context.Playlist.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlist.Remove(playlist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(int id)
        {
            return _context.Playlist.Any(e => e.Id == id);
        }


        private bool PlaylistSongExists(int id)
        {
            return _context.PlaylistSong.Any(e => e.Id == id);
        }
    }
}
