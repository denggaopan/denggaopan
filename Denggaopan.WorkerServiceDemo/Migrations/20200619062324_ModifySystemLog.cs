using Microsoft.EntityFrameworkCore.Migrations;

namespace Denggaopan.WorkerServiceDemo.Migrations
{
    public partial class ModifySystemLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "SystemLog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Module",
                table: "SystemLog");
        }
    }
}
