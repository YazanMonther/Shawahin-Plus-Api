using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShawahinAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImageforEvNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "EvNews",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "EvNews");
        }
    }
}
