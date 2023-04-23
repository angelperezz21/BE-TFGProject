using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class manyToManyBEandEB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeneficiariosSiguenEmpresa",
                columns: table => new
                {
                    IdBeneficiario = table.Column<int>(type: "int", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiariosSiguenEmpresa", x => new { x.IdBeneficiario, x.IdEmpresa });
                    table.ForeignKey(
                        name: "FK_BeneficiariosSiguenEmpresa_Beneficiarios_IdBeneficiario",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeneficiariosSiguenEmpresa_Empresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpresasSiguenBeneficiarios",
                columns: table => new
                {
                    IdBeneficiario = table.Column<int>(type: "int", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasSiguenBeneficiarios", x => new { x.IdBeneficiario, x.IdEmpresa });
                    table.ForeignKey(
                        name: "FK_EmpresasSiguenBeneficiarios_Beneficiarios_IdBeneficiario",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresasSiguenBeneficiarios_Empresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiariosSiguenEmpresa_IdEmpresa",
                table: "BeneficiariosSiguenEmpresa",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasSiguenBeneficiarios_IdEmpresa",
                table: "EmpresasSiguenBeneficiarios",
                column: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficiariosSiguenEmpresa");

            migrationBuilder.DropTable(
                name: "EmpresasSiguenBeneficiarios");
        }
    }
}
