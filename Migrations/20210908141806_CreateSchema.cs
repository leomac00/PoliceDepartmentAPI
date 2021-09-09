using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioAPI.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZIPCode = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coroners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    RegisterId = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coroners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perpetrators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perpetrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliceOfficers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    RegisterId = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceOfficers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    RegisterId = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Victims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Victims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliceDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdressId = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceDepartments_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Autopsies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VictimId = table.Column<int>(nullable: true),
                    CoronerId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autopsies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autopsies_Coroners_CoronerId",
                        column: x => x.CoronerId,
                        principalTable: "Coroners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Autopsies_Victims_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Victims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Crimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PerpetratorId = table.Column<int>(nullable: true),
                    VictimId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crimes_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Crimes_Perpetrators_PerpetratorId",
                        column: x => x.PerpetratorId,
                        principalTable: "Perpetrators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Crimes_Victims_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Victims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deputies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    PoliceDepartmentId = table.Column<int>(nullable: true),
                    ShiftCode = table.Column<int>(nullable: false),
                    RegisterId = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deputies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deputies_PoliceDepartments_PoliceDepartmentId",
                        column: x => x.PoliceDepartmentId,
                        principalTable: "PoliceDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arrests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OfficerId = table.Column<int>(nullable: true),
                    DeputyId = table.Column<int>(nullable: true),
                    CrimeId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arrests_Crimes_CrimeId",
                        column: x => x.CrimeId,
                        principalTable: "Crimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arrests_Deputies_DeputyId",
                        column: x => x.DeputyId,
                        principalTable: "Deputies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arrests_PoliceOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "PoliceOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arrests_CrimeId",
                table: "Arrests",
                column: "CrimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrests_DeputyId",
                table: "Arrests",
                column: "DeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrests_OfficerId",
                table: "Arrests",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Autopsies_CoronerId",
                table: "Autopsies",
                column: "CoronerId");

            migrationBuilder.CreateIndex(
                name: "IX_Autopsies_VictimId",
                table: "Autopsies",
                column: "VictimId");

            migrationBuilder.CreateIndex(
                name: "IX_Crimes_AdressId",
                table: "Crimes",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Crimes_PerpetratorId",
                table: "Crimes",
                column: "PerpetratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Crimes_VictimId",
                table: "Crimes",
                column: "VictimId");

            migrationBuilder.CreateIndex(
                name: "IX_Deputies_PoliceDepartmentId",
                table: "Deputies",
                column: "PoliceDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceDepartments_AdressId",
                table: "PoliceDepartments",
                column: "AdressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arrests");

            migrationBuilder.DropTable(
                name: "Autopsies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Crimes");

            migrationBuilder.DropTable(
                name: "Deputies");

            migrationBuilder.DropTable(
                name: "PoliceOfficers");

            migrationBuilder.DropTable(
                name: "Coroners");

            migrationBuilder.DropTable(
                name: "Perpetrators");

            migrationBuilder.DropTable(
                name: "Victims");

            migrationBuilder.DropTable(
                name: "PoliceDepartments");

            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
