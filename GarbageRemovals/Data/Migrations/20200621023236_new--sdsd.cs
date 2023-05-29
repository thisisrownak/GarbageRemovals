using Microsoft.EntityFrameworkCore.Migrations;

namespace GarbageRemovals.Data.Migrations
{
    public partial class newsdsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PickedAndDumps",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Managers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_AreaId",
                table: "Managers",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Areas_AreaId",
                table: "Managers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id"
);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Areas_AreaId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_AreaId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PickedAndDumps");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Managers");
        }
    }
}
