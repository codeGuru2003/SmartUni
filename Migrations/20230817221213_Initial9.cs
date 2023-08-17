using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentDegreeID",
                table: "EntranceApplicants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntryYear",
                table: "EntranceApplicants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scholarship",
                table: "EntranceApplicants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EntranceApplicantReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntranceApplicantID = table.Column<int>(type: "int", nullable: true),
                    RefereeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusTypeID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceApplicantReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntranceApplicantReferences_EntranceApplicants_EntranceApplicantID",
                        column: x => x.EntranceApplicantID,
                        principalTable: "EntranceApplicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntranceApplicantReferences_StatusTypes_StatusTypeID",
                        column: x => x.StatusTypeID,
                        principalTable: "StatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_DepartmentDegreeID",
                table: "EntranceApplicants",
                column: "DepartmentDegreeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicantReferences_EntranceApplicantID",
                table: "EntranceApplicantReferences",
                column: "EntranceApplicantID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicantReferences_StatusTypeID",
                table: "EntranceApplicantReferences",
                column: "StatusTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceApplicants_DepartmentDegrees_DepartmentDegreeID",
                table: "EntranceApplicants",
                column: "DepartmentDegreeID",
                principalTable: "DepartmentDegrees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceApplicants_DepartmentDegrees_DepartmentDegreeID",
                table: "EntranceApplicants");

            migrationBuilder.DropTable(
                name: "EntranceApplicantReferences");

            migrationBuilder.DropIndex(
                name: "IX_EntranceApplicants_DepartmentDegreeID",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "DepartmentDegreeID",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "EntryYear",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "Scholarship",
                table: "EntranceApplicants");
        }
    }
}
