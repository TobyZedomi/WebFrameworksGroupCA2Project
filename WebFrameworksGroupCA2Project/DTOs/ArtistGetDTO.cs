using System.ComponentModel.DataAnnotations;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.DTOs
{
    public class ArtistGetDTO
    {


        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9\s]*$", ErrorMessage = " Artist Name must Start with a capital Letter. Numbers and letters only, 100 characters maximum"), Required, StringLength(100)]
        public string ArtistName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z&/\s]*$", ErrorMessage = "Genre must start ith a capital letter and have 30 characters maximum. Only special characters allowed is &/"), Required, StringLength(30)]
        public string Genre { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Birth Country must start with a capital letter and have 50 characters maximum"), Required, StringLength(50)]
        public string BirthCountry { get; set; }

        [DataType(DataType.MultilineText)]
        public string Overview { get; set; }

        public List<Song>? Song { get; set; }

        public List<Vinyl>? Vinyl { get; set; }
    }
}
