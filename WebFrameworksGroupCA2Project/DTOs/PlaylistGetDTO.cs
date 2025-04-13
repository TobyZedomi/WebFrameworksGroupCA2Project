using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.DTOs
{
    public class PlaylistGetDTO
    {

        [Key]

        public int Id { get; set; }
        
        public string ImageFileName { get; set; } = string.Empty;

        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9\s]*$", ErrorMessage = "Playlist Name must Start with a capital Letter or number. Numbers and letters only, 30 characters maximum"), Required, StringLength(30)]
        public string PlaylistName { get; set; }

        public bool StatusPrivate { get; set; }
        public List<PlaylistSong>? PlaylistSongs { get; set; }
        
        

    }
}
