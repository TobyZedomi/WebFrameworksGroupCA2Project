using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Data
{
    public class WebFrameworksGroupCA2ProjectContext :  IdentityDbContext<AppUser>
    {
        public WebFrameworksGroupCA2ProjectContext (DbContextOptions<WebFrameworksGroupCA2ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<WebFrameworksGroupCA2Project.Models.Artist> Artist { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.Song> Song { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.Playlist> Playlist { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.PlaylistSong> PlaylistSong { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.Rating> Rating { get; set; } = default!;
    }
}
