using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class deleteBoolCertificado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certificado",
                table: "Recursos");

            migrationBuilder.DropColumn(
                name: "Certificado",
                table: "Necesidades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Certificado",
                table: "Recursos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Certificado",
                table: "Necesidades",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
