using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebFrameworksGroupCA2Project.Models
{
    public class AppUser : IdentityUser
    {

        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        public string? Address { get; set; }

        public List<Playlist>? Playlists { get; set; }

        public List<Rating>? Ratings { get; set; }

        public List<Purchase>? Purchases { get; set; }

        public List<UserVinylRequest>? UserVinylRequests { get; set; }

    }
}
