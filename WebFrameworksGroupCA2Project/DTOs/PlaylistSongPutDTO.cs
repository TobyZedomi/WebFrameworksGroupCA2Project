using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.DTOs
{
    public class PlaylistSongPutDTO
    {


        [Key]

        public int Id { get; set; }
        public int? PlaylistId { get; set; }
        [ForeignKey("PlaylistId")]
        public Playlist? Playlist { get; set; }
        public int? SongId { get; set; }
        [ForeignKey("SongId")]
        public Song? Song { get; set; }
    }
}
