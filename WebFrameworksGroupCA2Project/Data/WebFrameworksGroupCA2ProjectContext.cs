using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Data
{
    public class WebFrameworksGroupCA2ProjectContext : IdentityDbContext<AppUser>
    {
        public WebFrameworksGroupCA2ProjectContext(DbContextOptions<WebFrameworksGroupCA2ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<WebFrameworksGroupCA2Project.Models.Artist> Artist { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.Song> Song { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.Playlist> Playlist { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.PlaylistSong> PlaylistSong { get; set; } = default!;
        public DbSet<WebFrameworksGroupCA2Project.Models.Rating> Rating { get; set; } = default!;

        public DbSet<WebFrameworksGroupCA2Project.Models.Purchase> Purchases { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // on delete casacade 

            modelBuilder.Entity<Artist>()
               .HasMany(a => a.Song)
               .WithOne(ai => ai.Artist)
               .HasForeignKey(ai => ai.ArtistId)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Playlist>()
             .HasMany(a => a.PlaylistSongs)
             .WithOne(ai => ai.Playlist)
             .HasForeignKey(ai => ai.PlaylistId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Song>()
            .HasMany(a => a.PlaylistSongs)
            .WithOne(ai => ai.Song)
            .HasForeignKey(ai => ai.SongId)
            .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<Song>()
            .HasMany(a => a.Ratings)
            .WithOne(ai => ai.Song)
            .HasForeignKey(ai => ai.SongId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Artist>().HasData(

          new Artist()
          {
              Id = 1,
              ArtistName = "Jimi Hendrix",
              Genre = "Rock",
              BirthCountry = "America",
              DateOfBirth = new DateTime(1952, 12, 16),
              Overview = "An American guirtist and singer who is normally regarded as the greatest guitarist in music.",
              ImageFileName = "jimihendrix.jpg"
          },
           new Artist()
           {
               Id = 2,
               ArtistName = "Kendrick Lamar",
               Genre = "Hip Hop",
               BirthCountry = "America",
               DateOfBirth = new DateTime(1987, 10, 22),
               Overview = "Regarded as one of the gretaest rappers ever, that has timeless classic albums.",
               ImageFileName = "kendricklamar.jpg",
           },
           new Artist()
           {
               Id = 3,
               ArtistName = "Adele",
               Genre = "Pop",
               BirthCountry = "England",
               DateOfBirth = new DateTime(1990, 09, 12),
               Overview = "A super talaneted singer with varouos hits like Hello, Someone Like You and Rolling In The Deep.",
               ImageFileName = "adele.jpg",
           },
            new Artist()
            {
                Id = 4,
                ArtistName = "Prince",
                Genre = "R&B/Soul",
                BirthCountry = "America",
                DateOfBirth = new DateTime(1958, 06, 25),
                Overview = "Arguabaly the most taleneted artist ever. He can do it all sing, dance and play up to 27 instruments perfectly.",
                ImageFileName = "prince.jpg",
            },
             new Artist()
             {
                 Id = 5,
                 ArtistName = "David Bowie",
                 Genre = "Rock",
                 BirthCountry = "England",
                 DateOfBirth = new DateTime(1947, 03, 12),
                 Overview = "The man who created the album Ziggy Stardust.",
                 ImageFileName = "davidbowie.jpg",
             }

           );


            modelBuilder.Entity<Song>().HasData(

                new Song()
                {
                    Id = 1,
                    SongName = "All Along The WatchTower",
                    DateOfRelease = new DateTime(1967, 08, 12),
                    SongDescription = "A classic cover of Bob Dylans song where Jimi Hnedrix made the song his.",
                    ImageFileName = "allalongthewatchtower.jpg",
                    ArtistId = 1
                },
                 new Song()
                 {
                     Id = 2,
                     SongName = "i",
                     DateOfRelease = new DateTime(2015, 10, 12),
                     SongDescription = "The lead single for his classic album To Pimp A Butterly",
                     ImageFileName = "i.jpg",
                     ArtistId = 2
                 },
                  new Song()
                  {
                      Id = 3,
                      SongName = "Easy On Me",
                      DateOfRelease = new DateTime(2021, 04, 21),
                      SongDescription = "Adeles come back song after a 6 year hiatus.",
                      ImageFileName = "easyonme.jpg",
                      ArtistId = 3
                  },
                   new Song()
                   {
                       Id = 4,
                       SongName = "Purple Rain",
                       DateOfRelease = new DateTime(1984, 11, 14),
                       SongDescription = "AThe most iconic Prince song",
                       ImageFileName = "purplerain.jpg",
                       ArtistId = 4
                   },
                   new Song()
                   {
                       Id = 5,
                       SongName = "Sign O the Times",
                       DateOfRelease = new DateTime(1987, 02, 10),
                       SongDescription = "An era that people consider where Prince was at his best musically",
                       ImageFileName = "signothetimes.jpg",
                       ArtistId = 4
                   },
                    new Song()
                    {
                        Id = 6,
                        SongName = "Starman",
                        DateOfRelease = new DateTime(1972, 08, 11),
                        SongDescription = "David Bowie at the peak of his music career with a life changing song.",
                        ImageFileName = "starman.jpg",
                        ArtistId = 5
                    }

                );


            modelBuilder.Entity<Vinyl>().HasData(

                new Vinyl()
                {
                    Id = 1,
                    VinylName = "Electric Ladyland",
                    DateOfRelease = new DateTime(1968, 08, 22),
                    VinylInfo = "The third studio album by the artist Jimi Hnedrix",
                    ListPrice = 30,
                    ImageFileName = "EL.jpg",
                    ArtistId = 1
                },

                new Vinyl()
                {
                    Id = 2,
                    VinylName = "To Pimp A Butterfly",
                    DateOfRelease = new DateTime(2015, 03, 15),
                    VinylInfo = "The third studio album by the artist Kendrick Lamar",
                    ListPrice = 30,
                    ImageFileName = "tpab.jpg",
                    ArtistId = 2
                },

                 new Vinyl()
                 {
                     Id = 3,
                     VinylName = "30",
                     DateOfRelease = new DateTime(2015, 11, 25),
                     VinylInfo = "The comeback studio album by the artist Adele",
                     ListPrice = 20,
                     ImageFileName = "30.jpg",
                     ArtistId = 3
                 },

                  new Vinyl()
                  {
                      Id = 4,
                      VinylName = "Purple Rain",
                      DateOfRelease = new DateTime(1984, 09, 12),
                      VinylInfo = "The most popular album by the artist Prince",
                      ListPrice = 40,
                      ImageFileName = "pr.jpg",
                      ArtistId = 4
                  },

                  new Vinyl()
                  {
                      Id = 5,
                      VinylName = "Ziggy Stardust",
                      DateOfRelease = new DateTime(1973, 06, 09),
                      VinylInfo = "The most popular album by the artist David Bowie",
                      ListPrice = 20,
                      ImageFileName = "ZiggyStardust.jpg",
                      ArtistId = 5
                  }
                );




            base.OnModelCreating(modelBuilder);
        }
        public DbSet<WebFrameworksGroupCA2Project.Models.Vinyl> Vinyl { get; set; } = default!;
       
       

    }

}
