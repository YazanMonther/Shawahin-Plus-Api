using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShawahinAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbHors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSessions_ChargingStations_ChargingStationId",
                table: "ChargingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChargingStations_Chargers_ChargersId",
                table: "ChargingStations");

            migrationBuilder.DropForeignKey(
                name: "FK_ChargingStations_Contacts_ContactId",
                table: "ChargingStations");

            migrationBuilder.DropForeignKey(
                name: "FK_ChargingStations_Locations_LocationId",
                table: "ChargingStations");

            migrationBuilder.DropForeignKey(
                name: "FK_ChargingStations_StationOpeningHours_StationOpeningHoursId",
                table: "ChargingStations");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteStations_ChargingStations_StationId",
                table: "FavoriteStations");

            migrationBuilder.DropForeignKey(
                name: "FK_StationComments_ChargingStations_StationId",
                table: "StationComments");

            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_StationOpeningHours_TimeId",
                table: "StationRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StationOpeningHours",
                table: "StationOpeningHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChargingStations",
                table: "ChargingStations");

            migrationBuilder.RenameTable(
                name: "StationOpeningHours",
                newName: "chargingStationsHours");

            migrationBuilder.RenameTable(
                name: "ChargingStations",
                newName: "Stations");

            migrationBuilder.RenameIndex(
                name: "IX_ChargingStations_StationOpeningHoursId",
                table: "Stations",
                newName: "IX_Stations_StationOpeningHoursId");

            migrationBuilder.RenameIndex(
                name: "IX_ChargingStations_LocationId",
                table: "Stations",
                newName: "IX_Stations_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ChargingStations_ContactId",
                table: "Stations",
                newName: "IX_Stations_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_ChargingStations_ChargersId",
                table: "Stations",
                newName: "IX_Stations_ChargersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chargingStationsHours",
                table: "chargingStationsHours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stations",
                table: "Stations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingSessions_Stations_ChargingStationId",
                table: "ChargingSessions",
                column: "ChargingStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteStations_Stations_StationId",
                table: "FavoriteStations",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationComments_Stations_StationId",
                table: "StationComments",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_chargingStationsHours_TimeId",
                table: "StationRequest",
                column: "TimeId",
                principalTable: "chargingStationsHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Chargers_ChargersId",
                table: "Stations",
                column: "ChargersId",
                principalTable: "Chargers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Contacts_ContactId",
                table: "Stations",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Locations_LocationId",
                table: "Stations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_chargingStationsHours_StationOpeningHoursId",
                table: "Stations",
                column: "StationOpeningHoursId",
                principalTable: "chargingStationsHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSessions_Stations_ChargingStationId",
                table: "ChargingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteStations_Stations_StationId",
                table: "FavoriteStations");

            migrationBuilder.DropForeignKey(
                name: "FK_StationComments_Stations_StationId",
                table: "StationComments");

            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_chargingStationsHours_TimeId",
                table: "StationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Chargers_ChargersId",
                table: "Stations");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Contacts_ContactId",
                table: "Stations");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Locations_LocationId",
                table: "Stations");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_chargingStationsHours_StationOpeningHoursId",
                table: "Stations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stations",
                table: "Stations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chargingStationsHours",
                table: "chargingStationsHours");

            migrationBuilder.RenameTable(
                name: "Stations",
                newName: "ChargingStations");

            migrationBuilder.RenameTable(
                name: "chargingStationsHours",
                newName: "StationOpeningHours");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_StationOpeningHoursId",
                table: "ChargingStations",
                newName: "IX_ChargingStations_StationOpeningHoursId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_LocationId",
                table: "ChargingStations",
                newName: "IX_ChargingStations_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_ContactId",
                table: "ChargingStations",
                newName: "IX_ChargingStations_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_ChargersId",
                table: "ChargingStations",
                newName: "IX_ChargingStations_ChargersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChargingStations",
                table: "ChargingStations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StationOpeningHours",
                table: "StationOpeningHours",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingSessions_ChargingStations_ChargingStationId",
                table: "ChargingSessions",
                column: "ChargingStationId",
                principalTable: "ChargingStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingStations_Chargers_ChargersId",
                table: "ChargingStations",
                column: "ChargersId",
                principalTable: "Chargers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingStations_Contacts_ContactId",
                table: "ChargingStations",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingStations_Locations_LocationId",
                table: "ChargingStations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingStations_StationOpeningHours_StationOpeningHoursId",
                table: "ChargingStations",
                column: "StationOpeningHoursId",
                principalTable: "StationOpeningHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteStations_ChargingStations_StationId",
                table: "FavoriteStations",
                column: "StationId",
                principalTable: "ChargingStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationComments_ChargingStations_StationId",
                table: "StationComments",
                column: "StationId",
                principalTable: "ChargingStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_StationOpeningHours_TimeId",
                table: "StationRequest",
                column: "TimeId",
                principalTable: "StationOpeningHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
