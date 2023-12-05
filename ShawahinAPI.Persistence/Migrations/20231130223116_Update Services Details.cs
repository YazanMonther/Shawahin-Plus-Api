using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShawahinAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServicesDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactInformation",
                table: "ServiceInfo",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ServiceInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ServiceInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ServiceInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ServiceInfo");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ServiceInfo");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ServiceInfo");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ServiceInfo",
                newName: "ContactInformation");
        }
    }
}
