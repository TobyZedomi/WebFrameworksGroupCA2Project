using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFrameworksGroupCA2Project.Models
{
    public class Stock
    {

        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int? VinylId { get; set; }
        [ForeignKey("VinylId")]
        public Vinyl? Vinyl{ get; set; }

    }
}
