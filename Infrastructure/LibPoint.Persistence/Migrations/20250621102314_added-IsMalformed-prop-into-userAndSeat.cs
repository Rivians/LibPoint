using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibPoint.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedIsMalformedpropintouserAndSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMalformed",
                table: "Seats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMalformed",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMalformed",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "IsMalformed",
                table: "AspNetUsers");
        }
    }
}
