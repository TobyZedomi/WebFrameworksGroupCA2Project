using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebFrameworksGroupCA2Project.Migrations
{
    /// <inheritdoc />
    public partial class seedingArtistandSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "Id", "ArtistName", "BirthCountry", "DateOfBirth", "Genre", "ImageFileName", "Overview" },
                values: new object[,]
                {
                    { 1, "Jimi Hendrix", "America", new DateTime(1952, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rock", "jimihendrix.jpg", "An American guirtist and singer who is normally regarded as the greatest guitarist in music." },
                    { 2, "Kendrick Lamar", "America", new DateTime(1987, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hip Hop", "kendricklamar.jpg", "Regarded as one of the gretaest rappers ever, that has timeless classic albums." },
                    { 3, "Adele", "England", new DateTime(1990, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pop", "adele.jpg", "A super talaneted singer with varouos hits like Hello, Someone Like You and Rolling In The Deep." },
                    { 4, "Prince", "America", new DateTime(1958, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "R&B/Soul", "prince.jpg", "Arguabaly the most taleneted artist ever. He can do it all sing, dance and play up to 27 instruments perfectly." },
                    { 5, "David Bowie", "England", new DateTime(1947, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rock", "davidbowie.jpg", "The man who created the album Ziggy Stardust." }
                });

            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "Id", "ArtistId", "DateOfRelease", "ImageFileName", "SongDescription", "SongName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1967, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "allalongthewatchtower.jpg", "A classic cover of Bob Dylans song where Jimi Hnedrix made the song his.", "All Along The WatchTower" },
                    { 2, 2, new DateTime(2015, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "i.jpg", "The lead single for his classic album To Pimp A Butterly", "i" },
                    { 3, 3, new DateTime(2021, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "easyonme.jpg", "Adeles come back song after a 6 year hiatus.", "Easy On Me" },
                    { 4, 4, new DateTime(1984, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "purplerain.jpg", "AThe most iconic Prince song", "Purple Rain" },
                    { 5, 4, new DateTime(1987, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "signothetimes.jpg", "An era that people consider where Prince was at his best musically", "Sign O the Times" },
                    { 6, 5, new DateTime(1972, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "starman.jpg", "David Bowie at the peak of his music career with a life changing song.", "Starman" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
