using Microsoft.EntityFrameworkCore.Migrations;

namespace Denggaopan.GraphqlDemo.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Barcode = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    SellingPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Barcode);
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Barcode", "SellingPrice", "Title" },
                values: new object[] { "123", 50m, "Headphone" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Barcode", "SellingPrice", "Title" },
                values: new object[] { "456", 40m, "Keyboard" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Barcode", "SellingPrice", "Title" },
                values: new object[] { "789", 100m, "Monitor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
