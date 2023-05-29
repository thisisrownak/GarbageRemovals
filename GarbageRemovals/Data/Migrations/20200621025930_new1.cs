using Microsoft.EntityFrameworkCore.Migrations;

namespace GarbageRemovals.Data.Migrations
{
    public partial class new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PickedAndDumps");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Requests",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PickedAndDumps",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
