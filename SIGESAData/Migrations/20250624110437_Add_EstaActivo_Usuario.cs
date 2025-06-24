using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigesaData.Migrations
{
    /// <inheritdoc />
    public partial class Add_EstaActivo_Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaActivo",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaActivo",
                table: "Usuario");
        }
    }
}
