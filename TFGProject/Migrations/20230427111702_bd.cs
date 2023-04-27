using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class bd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1000),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasenya = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Web = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasenya = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Web = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Necesidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IdBeneficiario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Necesidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Necesidades_Beneficiarios_IdBeneficiario",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Donaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valorTotal = table.Column<double>(type: "float", nullable: false),
                    NombreRecurso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaDonacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdBeneficiario = table.Column<int>(type: "int", nullable: false),
                    IdCertificado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donaciones_Beneficiarios_IdBeneficiario",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donaciones_Certificados_IdCertificado",
                        column: x => x.IdCertificado,
                        principalTable: "Certificados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donaciones_Empresas_IdEmpresa",
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

            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    MetodoEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recursos_Empresas_IdEmpresa",
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
                name: "IX_Donaciones_IdBeneficiario",
                table: "Donaciones",
                column: "IdBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                unique: true,
                filter: "[IdCertificado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdEmpresa",
                table: "Donaciones",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasSiguenBeneficiarios_IdEmpresa",
                table: "EmpresasSiguenBeneficiarios",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Necesidades_IdBeneficiario",
                table: "Necesidades",
                column: "IdBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_IdEmpresa",
                table: "Recursos",
                column: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficiariosSiguenEmpresa");

            migrationBuilder.DropTable(
                name: "Donaciones");

            migrationBuilder.DropTable(
                name: "EmpresasSiguenBeneficiarios");

            migrationBuilder.DropTable(
                name: "Necesidades");

            migrationBuilder.DropTable(
                name: "Recursos");

            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropTable(
                name: "Beneficiarios");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
