using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioAPI.Migrations
{
    public partial class UpdArrest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerpetratorId",
                table: "Arrests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arrests_PerpetratorId",
                table: "Arrests",
                column: "PerpetratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrests_Perpetrators_PerpetratorId",
                table: "Arrests",
                column: "PerpetratorId",
                principalTable: "Perpetrators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrests_Perpetrators_PerpetratorId",
                table: "Arrests");

            migrationBuilder.DropIndex(
                name: "IX_Arrests_PerpetratorId",
                table: "Arrests");

            migrationBuilder.DropColumn(
                name: "PerpetratorId",
                table: "Arrests");
        }
    }
}
