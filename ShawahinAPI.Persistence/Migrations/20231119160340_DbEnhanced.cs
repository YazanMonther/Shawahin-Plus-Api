using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShawahinAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DbEnhanced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargerStatus",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "ChargerCost",
                table: "StationRequest");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "StationRequest");

            migrationBuilder.DropColumn(
                name: "ParkingType",
                table: "StationRequest");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StationRequest");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ChargerTypes",
                newName: "Charger_Type");

            migrationBuilder.RenameColumn(
                name: "ChargerImageUrl",
                table: "ChargerTypes",
                newName: "ChargerLogoUrl");

            migrationBuilder.AddColumn<int>(
                name: "Request_Status",
                table: "StationRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ChargerCost",
                table: "Chargers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChargerStatus",
                table: "Chargers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Chargers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParkingType",
                table: "Chargers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Chargers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Request_Status",
                table: "StationRequest");

            migrationBuilder.DropColumn(
                name: "ChargerCost",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "ChargerStatus",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "ParkingType",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Chargers");

            migrationBuilder.RenameColumn(
                name: "Charger_Type",
                table: "ChargerTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ChargerLogoUrl",
                table: "ChargerTypes",
                newName: "ChargerImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ChargerStatus",
                table: "Stations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Stations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ChargerCost",
                table: "StationRequest",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "StationRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParkingType",
                table: "StationRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StationRequest",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
