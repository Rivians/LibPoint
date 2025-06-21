using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibPoint.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedcodeintoborrowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Borrowings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Borrowings");
        }
    }
}
