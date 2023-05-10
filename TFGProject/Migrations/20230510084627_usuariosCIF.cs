using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class usuariosCIF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CIF",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CIF",
                table: "Beneficiarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CIF",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "CIF",
                table: "Beneficiarios");
        }
    }
}
