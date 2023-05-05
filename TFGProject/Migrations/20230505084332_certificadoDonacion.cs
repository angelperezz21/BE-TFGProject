using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class certificadoDonacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donaciones_Certificados_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones");

            migrationBuilder.DropColumn(
                name: "IdCertificado",
                table: "Donaciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCertificado",
                table: "Donaciones",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                unique: true,
                filter: "[IdCertificado] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Donaciones_Certificados_IdCertificado",
                table: "Donaciones",
                column: "IdCertificado",
                principalTable: "Certificados",
                principalColumn: "Id");
        }
    }
}
