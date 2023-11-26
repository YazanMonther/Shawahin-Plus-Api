using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShawahinAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DbUserId_CSRelatoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_Chargers_ChargerId",
                table: "StationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_Contacts_OwnerContactId",
                table: "StationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_chargingStationsHours_TimeId",
                table: "StationRequest");

            migrationBuilder.DropIndex(
                name: "IX_StationRequest_ChargerId",
                table: "StationRequest");

            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "StationRequest",
                newName: "StationOpeningHoursId");

            migrationBuilder.RenameColumn(
                name: "OwnerContactId",
                table: "StationRequest",
                newName: "ContactId");

            migrationBuilder.RenameColumn(
                name: "ChargerId",
                table: "StationRequest",
                newName: "ChargesId");

            migrationBuilder.RenameIndex(
                name: "IX_StationRequest_TimeId",
                table: "StationRequest",
                newName: "IX_StationRequest_StationOpeningHoursId");

            migrationBuilder.RenameIndex(
                name: "IX_StationRequest_OwnerContactId",
                table: "StationRequest",
                newName: "IX_StationRequest_ContactId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Stations",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ChargersId",
                table: "StationRequest",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_UserId",
                table: "Stations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StationRequest_ChargersId",
                table: "StationRequest",
                column: "ChargersId");

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_Chargers_ChargersId",
                table: "StationRequest",
                column: "ChargersId",
                principalTable: "Chargers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_Contacts_ContactId",
                table: "StationRequest",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_chargingStationsHours_StationOpeningHoursId",
                table: "StationRequest",
                column: "StationOpeningHoursId",
                principalTable: "chargingStationsHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_AspNetUsers_UserId",
                table: "Stations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_Chargers_ChargersId",
                table: "StationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_Contacts_ContactId",
                table: "StationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_StationRequest_chargingStationsHours_StationOpeningHoursId",
                table: "StationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_AspNetUsers_UserId",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_UserId",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_StationRequest_ChargersId",
                table: "StationRequest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "ChargersId",
                table: "StationRequest");

            migrationBuilder.RenameColumn(
                name: "StationOpeningHoursId",
                table: "StationRequest",
                newName: "TimeId");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "StationRequest",
                newName: "OwnerContactId");

            migrationBuilder.RenameColumn(
                name: "ChargesId",
                table: "StationRequest",
                newName: "ChargerId");

            migrationBuilder.RenameIndex(
                name: "IX_StationRequest_StationOpeningHoursId",
                table: "StationRequest",
                newName: "IX_StationRequest_TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_StationRequest_ContactId",
                table: "StationRequest",
                newName: "IX_StationRequest_OwnerContactId");

            migrationBuilder.CreateIndex(
                name: "IX_StationRequest_ChargerId",
                table: "StationRequest",
                column: "ChargerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_Chargers_ChargerId",
                table: "StationRequest",
                column: "ChargerId",
                principalTable: "Chargers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_Contacts_OwnerContactId",
                table: "StationRequest",
                column: "OwnerContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StationRequest_chargingStationsHours_TimeId",
                table: "StationRequest",
                column: "TimeId",
                principalTable: "chargingStationsHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
