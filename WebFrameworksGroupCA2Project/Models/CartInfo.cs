using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebFrameworksGroupCA2Project.Models
{
    public class CartInfo
    {

        [Key]   
        public int Id { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }
        public int? VinylId { get; set; }
        [ForeignKey("VinylId")]
        public Vinyl? Vinyl { get; set; }

        public int? ShoppingCartId { get; set; }
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart? ShoppingCart { get; set; }


    }
}
