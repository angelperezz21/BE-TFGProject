using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class oneToManyRENB : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Recursos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdBeneficiario",
                table: "Necesidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdCertificado",
                table: "Donaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_IdEmpresa",
                table: "Recursos",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Necesidades_IdBeneficiario",
                table: "Necesidades",
                column: "IdBeneficiario");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Necesidades_Beneficiarios_IdBeneficiario",
                table: "Necesidades",
                column: "IdBeneficiario",
                principalTable: "Beneficiarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recursos_Empresas_IdEmpresa",
                table: "Recursos",
                column: "IdEmpresa",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Certificado_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Necesidades_Beneficiarios_IdBeneficiario",
                table: "Necesidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Recursos_Empresas_IdEmpresa",
                table: "Recursos");

            migrationBuilder.DropIndex(
                name: "IX_Recursos_IdEmpresa",
                table: "Recursos");

            migrationBuilder.DropIndex(
                name: "IX_Necesidades_IdBeneficiario",
                table: "Necesidades");

            migrationBuilder.DropIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Recursos");

            migrationBuilder.DropColumn(
                name: "IdBeneficiario",
                table: "Necesidades");

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
