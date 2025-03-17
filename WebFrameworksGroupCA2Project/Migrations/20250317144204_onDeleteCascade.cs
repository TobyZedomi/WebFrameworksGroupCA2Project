using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFrameworksGroupCA2Project.Migrations
{
    /// <inheritdoc />
    public partial class onDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Playlist_PlaylistId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Song_SongId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Song_SongId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Playlist_PlaylistId",
                table: "PlaylistSong",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Song_SongId",
                table: "PlaylistSong",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Song_SongId",
                table: "Rating",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Playlist_PlaylistId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Song_SongId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Song_SongId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Playlist_PlaylistId",
                table: "PlaylistSong",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Song_SongId",
                table: "PlaylistSong",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Song_SongId",
                table: "Rating",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id");
        }
    }
}
