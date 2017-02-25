using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaperID.Migrations
{
    public partial class InitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaggedItems",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: true),
                    SalePrice = table.Column<decimal>(type: "Money", nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaggedItems", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaggedItems");
        }
    }
}
