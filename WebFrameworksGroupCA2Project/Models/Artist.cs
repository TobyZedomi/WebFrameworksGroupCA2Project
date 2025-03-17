using System.ComponentModel.DataAnnotations;

namespace WebFrameworksGroupCA2Project.Models
{
    public class Artist
    {

        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9\s]*$", ErrorMessage = " Artist Name must Start with a capital Letter. Numbers and letters only, 100 characters maximum"), Required, StringLength(100)]
        public string ArtistName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z&/\s]*$", ErrorMessage = "Genre must start ith a capital letter and have 30 characters maximum. Only special characters allowed is &/"), Required, StringLength(30)]
        public string Genre { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(50)]
        public string BirthCountry { get; set; }

        [Display(Name = "DateOfBirth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.MultilineText)]
        public string Overview { get; set; }

        public string ImageFileName { get; set; }

        public List<Song>? Song { get; set; }


    }
}
