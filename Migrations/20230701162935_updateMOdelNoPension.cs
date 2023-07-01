using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplicativo.net.Migrations
{
    public partial class updateMOdelNoPension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Nopension",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "estadoCertificado",
                table: "Nopension",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaVencimiento",
                table: "Nopension",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "Nopension",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "totalDescargas",
                table: "Nopension",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "Nopension",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Nopension");

            migrationBuilder.DropColumn(
                name: "estadoCertificado",
                table: "Nopension");

            migrationBuilder.DropColumn(
                name: "fechaVencimiento",
                table: "Nopension");

            migrationBuilder.DropColumn(
                name: "token",
                table: "Nopension");

            migrationBuilder.DropColumn(
                name: "totalDescargas",
                table: "Nopension");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Nopension");
        }
    }
}
