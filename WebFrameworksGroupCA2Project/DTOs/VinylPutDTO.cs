using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.DTOs
{
    public class VinylPutDTO
    {

        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9\s]*$", ErrorMessage = "Vinyl Name must Start with a capital Letter or number. Numbers and letters only, 30 characters maximum"), Required, StringLength(30)]

        public string VinylName { get; set; }

        [Display(Name = "DateOfRelease"), DataType(DataType.Date)]
        public DateTime DateOfRelease { get; set; }

        public double ListPrice { get; set; }

        [DataType(DataType.MultilineText)]
        public string? VinylInfo { get; set; }
        public IFormFile? ImageFile { get; set; }

        public int? ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public Artist? Artist { get; set; }
    }
}
