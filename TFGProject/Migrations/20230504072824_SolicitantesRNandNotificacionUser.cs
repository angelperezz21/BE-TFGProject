using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class SolicitantesRNandNotificacionUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdSolicitante",
                table: "Recursos");

            migrationBuilder.AddColumn<string>(
                name: "Solicitantes",
                table: "Recursos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Solicitantes",
                table: "Necesidades",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imgUrl",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Notificacion",
                table: "Empresas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imgUrl",
                table: "Beneficiarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Beneficiarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Notificacion",
                table: "Beneficiarios",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solicitantes",
                table: "Recursos");

            migrationBuilder.DropColumn(
                name: "Solicitantes",
                table: "Necesidades");

            migrationBuilder.DropColumn(
                name: "Notificacion",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Notificacion",
                table: "Beneficiarios");

            migrationBuilder.AddColumn<int>(
                name: "IdSolicitante",
                table: "Recursos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imgUrl",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imgUrl",
                table: "Beneficiarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Beneficiarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
