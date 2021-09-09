using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioAPI.Migrations
{
    public partial class UpdDeputy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShiftCode",
                table: "Deputies");

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "Deputies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Deputies");

            migrationBuilder.AddColumn<int>(
                name: "ShiftCode",
                table: "Deputies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
