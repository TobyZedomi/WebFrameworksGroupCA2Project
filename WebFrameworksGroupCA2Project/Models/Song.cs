using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebFrameworksGroupCA2Project.Models
{
    public class Song
    {

        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9\s]*$", ErrorMessage = "Song Name must Start with a capital Letter or number. Numbers and letters only, 30 characters maximum"), Required, StringLength(30)]

        public string SongName { get; set; }

        [Display(Name = "DateOfRelease"), DataType(DataType.Date)]
        public DateTime DateOfRelease { get; set; }

        [DataType(DataType.MultilineText)]
        public string SongDescription { get; set; }

        public string ImageFileName { get; set; }

        public int? ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public Artist? Artist { get; set; }

    }
}
