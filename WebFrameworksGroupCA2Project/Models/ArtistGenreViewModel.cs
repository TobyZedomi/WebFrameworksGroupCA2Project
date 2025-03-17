using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebFrameworksGroupCA2Project.Models
{
    public class ArtistGenreViewModel
    {

        public List<Artist>? Artists { get; set; }
        public SelectList? Genres { get; set; }
        public string? ArtistGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
