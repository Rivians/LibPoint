using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibPoint.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedauthorNameintobook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Books");
        }
    }
}
