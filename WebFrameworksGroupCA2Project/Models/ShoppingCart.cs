using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace WebFrameworksGroupCA2Project.Models
{
    public class ShoppingCart
    {

        [Key]   

        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }

        public List<CartInfo>? CartInfos { get; set; }
    }
}
