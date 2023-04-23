using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class updateOneToOneDonacionCertificado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones");

            migrationBuilder.RenameColumn(
                name: "IdDonacion",
                table: "Certificado",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "IdCertificado",
                table: "Donaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                unique: true,
                filter: "[IdCertificado] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                principalTable: "Certificado",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Certificado",
                newName: "IdDonacion");

            migrationBuilder.AlterColumn<int>(
                name: "IdCertificado",
                table: "Donaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                principalTable: "Certificado",
                principalColumn: "IdDonacion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
