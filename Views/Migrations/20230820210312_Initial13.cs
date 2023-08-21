using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollegeID",
                table: "EntranceApplicants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "EntranceApplicants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CollegeID",
                table: "Students",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentID",
                table: "Students",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_CollegeID",
                table: "EntranceApplicants",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_DepartmentID",
                table: "EntranceApplicants",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceApplicants_College_CollegeID",
                table: "EntranceApplicants",
                column: "CollegeID",
                principalTable: "College",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceApplicants_Departments_DepartmentID",
                table: "EntranceApplicants",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_College_CollegeID",
                table: "Students",
                column: "CollegeID",
                principalTable: "College",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentID",
                table: "Students",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceApplicants_College_CollegeID",
                table: "EntranceApplicants");

            migrationBuilder.DropForeignKey(
                name: "FK_EntranceApplicants_Departments_DepartmentID",
                table: "EntranceApplicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_College_CollegeID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CollegeID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_EntranceApplicants_CollegeID",
                table: "EntranceApplicants");

            migrationBuilder.DropIndex(
                name: "IX_EntranceApplicants_DepartmentID",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "CollegeID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CollegeID",
                table: "EntranceApplicants");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "EntranceApplicants");
        }
    }
}
