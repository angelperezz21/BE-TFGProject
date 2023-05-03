using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class IdSolicitanteRN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSolicitante",
                table: "Recursos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdSolicitante",
                table: "Necesidades",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdSolicitante",
                table: "Recursos");

            migrationBuilder.DropColumn(
                name: "IdSolicitante",
                table: "Necesidades");
        }
    }
}
