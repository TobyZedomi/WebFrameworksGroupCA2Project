using System.ComponentModel.DataAnnotations;

namespace WebFrameworksGroupCA2Project.Models
{
    public class ShoppingCartItem
    {

        [Key]

        public int Id { get; set; }

        public Vinyl Vinyl { get; set; }

        public int Quantity { get; set; }

    }
}
