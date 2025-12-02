using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalBackend.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReservationLogs_ReservationId",
                table: "ReservationLogs",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationLogs_UserId",
                table: "ReservationLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationLogs_Reservations_ReservationId",
                table: "ReservationLogs",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationLogs_Users_UserId",
                table: "ReservationLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationLogs_Reservations_ReservationId",
                table: "ReservationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationLogs_Users_UserId",
                table: "ReservationLogs");

            migrationBuilder.DropIndex(
                name: "IX_ReservationLogs_ReservationId",
                table: "ReservationLogs");

            migrationBuilder.DropIndex(
                name: "IX_ReservationLogs_UserId",
                table: "ReservationLogs");
        }
    }
}
