using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibPoint.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedcheckinTokenintoreservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckInToken",
                table: "Reservations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInToken",
                table: "Reservations");
        }
    }
}
