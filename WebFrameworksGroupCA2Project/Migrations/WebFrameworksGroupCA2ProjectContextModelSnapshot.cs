﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebFrameworksGroupCA2Project.Data;

#nullable disable

namespace WebFrameworksGroupCA2Project.Migrations
{
    [DbContext(typeof(WebFrameworksGroupCA2ProjectContext))]
    partial class WebFrameworksGroupCA2ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BirthCountry")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artist");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArtistName = "Jimi Hendrix",
                            BirthCountry = "America",
                            DateOfBirth = new DateTime(1952, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Rock",
                            ImageFileName = "jimihendrix.jpg",
                            Overview = "An American guirtist and singer who is normally regarded as the greatest guitarist in music."
                        },
                        new
                        {
                            Id = 2,
                            ArtistName = "Kendrick Lamar",
                            BirthCountry = "America",
                            DateOfBirth = new DateTime(1987, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Hip Hop",
                            ImageFileName = "kendricklamar.jpg",
                            Overview = "Regarded as one of the gretaest rappers ever, that has timeless classic albums."
                        },
                        new
                        {
                            Id = 3,
                            ArtistName = "Adele",
                            BirthCountry = "England",
                            DateOfBirth = new DateTime(1990, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Pop",
                            ImageFileName = "adele.jpg",
                            Overview = "A super talaneted singer with varouos hits like Hello, Someone Like You and Rolling In The Deep."
                        },
                        new
                        {
                            Id = 4,
                            ArtistName = "Prince",
                            BirthCountry = "America",
                            DateOfBirth = new DateTime(1958, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "R&B/Soul",
                            ImageFileName = "prince.jpg",
                            Overview = "Arguabaly the most taleneted artist ever. He can do it all sing, dance and play up to 27 instruments perfectly."
                        },
                        new
                        {
                            Id = 5,
                            ArtistName = "David Bowie",
                            BirthCountry = "England",
                            DateOfBirth = new DateTime(1947, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Rock",
                            ImageFileName = "davidbowie.jpg",
                            Overview = "The man who created the album Ziggy Stardust."
                        });
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaylistName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("StatusPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.PlaylistSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int?>("SongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("SongId");

                    b.ToTable("PlaylistSong");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VinylId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VinylId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SongId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("UserRating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SongId");

                    b.HasIndex("UserId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArtistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfRelease")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SongDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Song");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArtistId = 1,
                            DateOfRelease = new DateTime(1967, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "allalongthewatchtower.jpg",
                            SongDescription = "A classic cover of Bob Dylans song where Jimi Hnedrix made the song his.",
                            SongName = "All Along The WatchTower"
                        },
                        new
                        {
                            Id = 2,
                            ArtistId = 2,
                            DateOfRelease = new DateTime(2015, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "i.jpg",
                            SongDescription = "The lead single for his classic album To Pimp A Butterly",
                            SongName = "i"
                        },
                        new
                        {
                            Id = 3,
                            ArtistId = 3,
                            DateOfRelease = new DateTime(2021, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "easyonme.jpg",
                            SongDescription = "Adeles come back song after a 6 year hiatus.",
                            SongName = "Easy On Me"
                        },
                        new
                        {
                            Id = 4,
                            ArtistId = 4,
                            DateOfRelease = new DateTime(1984, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "purplerain.jpg",
                            SongDescription = "AThe most iconic Prince song",
                            SongName = "Purple Rain"
                        },
                        new
                        {
                            Id = 5,
                            ArtistId = 4,
                            DateOfRelease = new DateTime(1987, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "signothetimes.jpg",
                            SongDescription = "An era that people consider where Prince was at his best musically",
                            SongName = "Sign O the Times"
                        },
                        new
                        {
                            Id = 6,
                            ArtistId = 5,
                            DateOfRelease = new DateTime(1972, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "starman.jpg",
                            SongDescription = "David Bowie at the peak of his music career with a life changing song.",
                            SongName = "Starman"
                        });
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Vinyl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArtistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfRelease")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<string>("VinylInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VinylName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Vinyl");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArtistId = 1,
                            DateOfRelease = new DateTime(1968, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "EL.jpg",
                            ListPrice = 30.0,
                            VinylInfo = "The third studio album by the artist Jimi Hnedrix",
                            VinylName = "Electric Ladyland"
                        },
                        new
                        {
                            Id = 2,
                            ArtistId = 2,
                            DateOfRelease = new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "tpab.jpg",
                            ListPrice = 30.0,
                            VinylInfo = "The third studio album by the artist Kendrick Lamar",
                            VinylName = "To Pimp A Butterfly"
                        },
                        new
                        {
                            Id = 3,
                            ArtistId = 3,
                            DateOfRelease = new DateTime(2015, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "30.jpg",
                            ListPrice = 20.0,
                            VinylInfo = "The comeback studio album by the artist Adele",
                            VinylName = "30"
                        },
                        new
                        {
                            Id = 4,
                            ArtistId = 4,
                            DateOfRelease = new DateTime(1984, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "pr.jpg",
                            ListPrice = 40.0,
                            VinylInfo = "The most popular album by the artist Prince",
                            VinylName = "Purple Rain"
                        },
                        new
                        {
                            Id = 5,
                            ArtistId = 5,
                            DateOfRelease = new DateTime(1973, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageFileName = "ZiggyStardust.jpg",
                            ListPrice = 20.0,
                            VinylInfo = "The most popular album by the artist David Bowie",
                            VinylName = "Ziggy Stardust"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebFrameworksGroupCA2Project.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Playlist", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.AppUser", "AppUser")
                        .WithMany("Playlists")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.PlaylistSong", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.Playlist", "Playlist")
                        .WithMany("PlaylistSongs")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebFrameworksGroupCA2Project.Models.Song", "Song")
                        .WithMany("PlaylistSongs")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Playlist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Purchase", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.AppUser", "AppUser")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId");

                    b.HasOne("WebFrameworksGroupCA2Project.Models.Vinyl", "Vinyl")
                        .WithMany()
                        .HasForeignKey("VinylId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Vinyl");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Rating", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.Song", "Song")
                        .WithMany("Ratings")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebFrameworksGroupCA2Project.Models.AppUser", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId");

                    b.Navigation("Song");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Song", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.Artist", "Artist")
                        .WithMany("Song")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Vinyl", b =>
                {
                    b.HasOne("WebFrameworksGroupCA2Project.Models.Artist", "Artist")
                        .WithMany("Vinyl")
                        .HasForeignKey("ArtistId");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.AppUser", b =>
                {
                    b.Navigation("Playlists");

                    b.Navigation("Purchases");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Artist", b =>
                {
                    b.Navigation("Song");

                    b.Navigation("Vinyl");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Playlist", b =>
                {
                    b.Navigation("PlaylistSongs");
                });

            modelBuilder.Entity("WebFrameworksGroupCA2Project.Models.Song", b =>
                {
                    b.Navigation("PlaylistSongs");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
