using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class AddLevelTypeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelTypeID",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LevelTypeID",
                table: "Courses",
                column: "LevelTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_LevelTypes_LevelTypeID",
                table: "Courses",
                column: "LevelTypeID",
                principalTable: "LevelTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_LevelTypes_LevelTypeID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LevelTypeID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LevelTypeID",
                table: "Courses");
        }
    }
}
