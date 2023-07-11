using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplicativo.net.Migrations
{
    public partial class addnewCOlumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "qrPath",
                table: "Nopension",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "qrPath",
                table: "Nopension");
        }
    }
}
