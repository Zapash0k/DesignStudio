using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignStudio.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PortfolioItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PortfolioItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
