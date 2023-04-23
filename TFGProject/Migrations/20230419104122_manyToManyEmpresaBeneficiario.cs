using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class manyToManyEmpresaBeneficiario : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "IdCertificado",
                table: "Donaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdBeneficiario",
                table: "Donaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Donaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdBeneficiario",
                table: "Donaciones",
                column: "IdBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdEmpresa",
                table: "Donaciones",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Beneficiarios_IdBeneficiario",
                table: "Donaciones",
                column: "IdBeneficiario",
                principalTable: "Beneficiarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                principalTable: "Certificado",
                principalColumn: "IdDonacion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Empresas_IdEmpresa",
                table: "Donaciones",
                column: "IdEmpresa",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Beneficiarios_IdBeneficiario",
                table: "Donaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Empresas_IdEmpresa",
                table: "Donaciones");

            migrationBuilder.DropIndex(
                name: "IX_Donaciones_IdBeneficiario",
                table: "Donaciones");

            migrationBuilder.DropIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropIndex(
                name: "IX_Donaciones_IdEmpresa",
                table: "Donaciones");

            migrationBuilder.DropColumn(
                name: "IdBeneficiario",
                table: "Donaciones");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Donaciones");

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
                principalColumn: "IdDonacion");
        }
    }
}
