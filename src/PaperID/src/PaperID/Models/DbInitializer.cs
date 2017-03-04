using PaperID.Data;
using PaperID.DataModels;
using System.Linq;

namespace PaperID.Models
{
    public class DbInitializer
    {
        public static void Initialize(ShopSightContext context)
        {
            context.Database.EnsureCreated();
            if (context.TaggedItems.Any())
            {
                return;
            }

            var items = new TaggedItem[]
            {
                new TaggedItem()
                {
                    ID = "a001",
                    ImageUrl = "/images/sample/a001.jpg",
                    Manufacturer = "Nordstrom",
                    Price = (decimal)50.00,
                    SalePrice = (decimal)48.00,
                    Title = "Blue Shirt"
                },
                new TaggedItem()
                {
                    ID = "a002",
                    ImageUrl = "/images/sample/a002.jpg",
                    Manufacturer = "Giordano",
                    Price = (decimal)60.00,
                    SalePrice = (decimal)55.00,
                    Title = "White Shirt"
                },
                new TaggedItem()
                {
                    ID = "a003",
                    ImageUrl = "/images/sample/a003.jpg",
                    Manufacturer = "Ralph Lauren",
                    Price = (decimal)45.00,
                    SalePrice = null,
                    Title = "White Shirt"
                },
                new TaggedItem()
                {
                    ID = "a004",
                    ImageUrl = "/images/sample/a004.jpg",
                    Manufacturer = "Levis",
                    Price = (decimal)30.00,
                    SalePrice = (decimal)28.00,
                    Title = "Blue Jeans"
                },
                new TaggedItem()
                {
                    ID = "a005",
                    ImageUrl = "/images/sample/a005.jpg",
                    Manufacturer = "Levis",
                    Price = (decimal)40.00,
                    SalePrice = (decimal)38.00,
                    Title = "Tan Jeans"
                },
                new TaggedItem()
                {
                    ID = "a006",
                    ImageUrl = "/images/sample/a006.jpg",
                    Manufacturer = "Levis",
                    Price = (decimal)999.00,
                    SalePrice = (decimal)599.00,
                    Title = "Coat"
                },
            };

            foreach (var item in items)
            {
                context.TaggedItems.Add(item);
            }

            context.SaveChanges();
        }
    }
}
