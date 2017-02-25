namespace PaperID.Data
{
    using Microsoft.EntityFrameworkCore;
    using PaperID.DataModels;

    public class ShopSightContext : DbContext
    {
        public ShopSightContext(DbContextOptions<ShopSightContext> options)
            : base(options)
        {
        }

        public DbSet<TaggedItem> TaggedItems { get; set; }
    }
}
