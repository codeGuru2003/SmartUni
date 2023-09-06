using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class FacultyTableModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderID",
                table: "Faculties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Faculties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TitleID",
                table: "Faculties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_GenderID",
                table: "Faculties",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_GroupID",
                table: "Faculties",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_TitleID",
                table: "Faculties",
                column: "TitleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Genders_GenderID",
                table: "Faculties",
                column: "GenderID",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Groups_GroupID",
                table: "Faculties",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_TitleTypes_TitleID",
                table: "Faculties",
                column: "TitleID",
                principalTable: "TitleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Genders_GenderID",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Groups_GroupID",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_TitleTypes_TitleID",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_GenderID",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_GroupID",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_TitleID",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "GenderID",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "TitleID",
                table: "Faculties");
        }
    }
}
