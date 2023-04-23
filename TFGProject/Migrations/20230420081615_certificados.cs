using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class certificados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificado",
                table: "Certificado");

            migrationBuilder.RenameTable(
                name: "Certificado",
                newName: "Certificados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificados",
                table: "Certificados",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Certificados_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                principalTable: "Certificados",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Certificados_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificados",
                table: "Certificados");

            migrationBuilder.RenameTable(
                name: "Certificados",
                newName: "Certificado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificado",
                table: "Certificado",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                principalTable: "Certificado",
                principalColumn: "Id");
        }
    }
}
