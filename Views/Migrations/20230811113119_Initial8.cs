using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfferingTypeID",
                table: "EntranceApplicants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DepartmentDegrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDegrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentDegrees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentMajors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMajors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentMajors_College_CollegeID",
                        column: x => x.CollegeID,
                        principalTable: "College",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_OfferingTypeID",
                table: "EntranceApplicants",
                column: "OfferingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentDegrees_DepartmentID",
                table: "DepartmentDegrees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMajors_CollegeID",
                table: "DepartmentMajors",
                column: "CollegeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceApplicants_OfferingTypes_OfferingTypeID",
                table: "EntranceApplicants",
                column: "OfferingTypeID",
                principalTable: "OfferingTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceApplicants_OfferingTypes_OfferingTypeID",
                table: "EntranceApplicants");

            migrationBuilder.DropTable(
                name: "DepartmentDegrees");

            migrationBuilder.DropTable(
                name: "DepartmentMajors");

            migrationBuilder.DropIndex(
                name: "IX_EntranceApplicants_OfferingTypeID",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "OfferingTypeID",
                table: "EntranceApplicants");
        }
    }
}
