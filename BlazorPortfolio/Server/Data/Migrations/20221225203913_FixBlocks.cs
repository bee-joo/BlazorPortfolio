using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorPortfolio.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBlocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Blocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Blocks");
        }
    }
}
