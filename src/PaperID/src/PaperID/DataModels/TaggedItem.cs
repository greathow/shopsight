using System.ComponentModel.DataAnnotations.Schema;

namespace PaperID.DataModels
{
    public class TaggedItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }

        public string Title { get; set; }
        
        public string Manufacturer { get; set; }

        [Column(TypeName="Money")]
        public decimal? Price { get; set; }

        [Column(TypeName="Money")]
        public decimal? SalePrice { get; set; }

        public string ImageUrl { get; set; }
    }
}
