using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebFrameworksGroupCA2Project.Models
{
    public class PlaylistSong
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
