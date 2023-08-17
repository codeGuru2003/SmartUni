using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenToThisSchoolBefore",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "OutOfHighSchool",
                table: "EntranceApplicants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenToThisSchoolBefore",
                table: "EntranceApplicants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutOfHighSchool",
                table: "EntranceApplicants",
                type: "bit",
                nullable: true);
        }
    }
}
