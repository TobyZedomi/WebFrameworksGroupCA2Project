using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFrameworksGroupCA2Project.Models
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int? PricePerUnit { get; set; }

        public int VinylId { get; set; }

        public Vinyl Vinyl { get; set; }

        public int? PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public Purchase? Purchase { get; set; }
    }
}
