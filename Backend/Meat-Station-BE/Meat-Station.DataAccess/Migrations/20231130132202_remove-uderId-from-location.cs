using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meat_Station.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeuderIdfromlocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Locations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Locations",
                type: "int",
                nullable: true);
        }
    }
}
