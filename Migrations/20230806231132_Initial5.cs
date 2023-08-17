using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountyOfHighSchoolAttended",
                table: "EntranceApplicants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndYear",
                table: "EntranceApplicants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenToThisSchoolBefore",
                table: "EntranceApplicants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HighSchoolAttendedName",
                table: "EntranceApplicants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfUniversity",
                table: "EntranceApplicants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutOfHighSchool",
                table: "EntranceApplicants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartYear",
                table: "EntranceApplicants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityCountryID",
                table: "EntranceApplicants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UniversityEndYear",
                table: "EntranceApplicants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UniversityStartYear",
                table: "EntranceApplicants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_UniversityCountryID",
                table: "EntranceApplicants",
                column: "UniversityCountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceApplicants_CountryTypes_UniversityCountryID",
                table: "EntranceApplicants",
                column: "UniversityCountryID",
                principalTable: "CountryTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceApplicants_CountryTypes_UniversityCountryID",
                table: "EntranceApplicants");

            migrationBuilder.DropIndex(
                name: "IX_EntranceApplicants_UniversityCountryID",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "CountyOfHighSchoolAttended",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "HasBeenToThisSchoolBefore",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "HighSchoolAttendedName",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "NameOfUniversity",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "OutOfHighSchool",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "UniversityCountryID",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "UniversityEndYear",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "UniversityStartYear",
                table: "EntranceApplicants");
        }
    }
}
