using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFGProject.Migrations
{
    /// <inheritdoc />
    public partial class descripcionNecesidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Necesidades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Necesidades");
        }
    }
}
