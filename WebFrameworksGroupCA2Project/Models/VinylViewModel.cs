using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebFrameworksGroupCA2Project.Models
{
    public class VinylViewModel
    {

        public List<Vinyl>? Vinyls { get; set; }

        public SelectList? ListPrice { get; set; }
        public double? VinylListPrice { get; set; }
        public string? SearchString { get; set; }

    }
}
