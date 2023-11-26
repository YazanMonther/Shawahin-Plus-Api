using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShawahinAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DbEnhanced2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargerSpeed",
                table: "Chargers");

            migrationBuilder.AlterColumn<int>(
                name: "PowerKw",
                table: "Chargers",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "CurrentChargerStatus",
                table: "Chargers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Chargers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentChargerStatus",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Chargers");

            migrationBuilder.AlterColumn<double>(
                name: "PowerKw",
                table: "Chargers",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ChargerSpeed",
                table: "Chargers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
