using System.ComponentModel.DataAnnotations;

namespace PaperID.Models.TagManagerViewModels
{
    public class TagViewModel
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Manufacturer { get; set; }
        
        public decimal? Price { get; set; }
        
        public decimal? SalePrice { get; set; }

        public string ImageUrl { get; set; }
    }
}
