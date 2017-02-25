using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PaperID.Data;

namespace PaperID.Migrations
{
    [DbContext(typeof(ShopSightContext))]
    partial class ShopSightContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaperID.DataModels.TaggedItem", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Manufacturer");

                    b.Property<decimal?>("Price")
                        .HasColumnType("Money");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("Money");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("TaggedItems");
                });
        }
    }
}
