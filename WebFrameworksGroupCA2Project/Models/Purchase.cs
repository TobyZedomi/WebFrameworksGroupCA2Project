using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFrameworksGroupCA2Project.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        public DateTime? PurchaseDate{ get; set; }

        public decimal? Total {  get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }

       public List<OrderItems>? OrderItems { get; set; }

    }
}
