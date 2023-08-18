using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "Students",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CountyOfHighSchoolAttended",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentDegreeID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndYear",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntryYear",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HighSchoolAttendedName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfUniversity",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferingTypeID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scholarship",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartYear",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityCountryID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UniversityEndYear",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UniversityStartYear",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentTypeID",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DepartmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacultyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentMajorMinior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentMajorId = table.Column<int>(type: "int", nullable: false),
                    DepartmentMiniorId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMajorMinior", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMajorMinior_Departments_DepartmentMajorId",
                        column: x => x.DepartmentMajorId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentMajorMinior_Departments_DepartmentMiniorId",
                        column: x => x.DepartmentMiniorId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentMajorMinior_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyTypeID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalityID = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusTypeID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculties_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Faculties_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faculties_FacultyTypes_FacultyTypeID",
                        column: x => x.FacultyTypeID,
                        principalTable: "FacultyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Faculties_MaritalStatusTypes_MaritalStatusTypeID",
                        column: x => x.MaritalStatusTypeID,
                        principalTable: "MaritalStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faculties_NationalityTypes_NationalityID",
                        column: x => x.NationalityID,
                        principalTable: "NationalityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    SectionTypeId = table.Column<int>(type: "int", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    AcademicSemesterId = table.Column<int>(type: "int", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Occupant = table.Column<int>(type: "int", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectionTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_AcademicSemesters_AcademicSemesterId",
                        column: x => x.AcademicSemesterId,
                        principalTable: "AcademicSemesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sections_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_SectionTypes_SectionTypeId",
                        column: x => x.SectionTypeId,
                        principalTable: "SectionTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    AcademicSemesterId = table.Column<int>(type: "int", nullable: true),
                    StatusTypesId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSections_AcademicSemesters_AcademicSemesterId",
                        column: x => x.AcademicSemesterId,
                        principalTable: "AcademicSemesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSections_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSections_StatusTypes_StatusTypesId",
                        column: x => x.StatusTypesId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSections_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentDegreeID",
                table: "Students",
                column: "DepartmentDegreeID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_OfferingTypeID",
                table: "Students",
                column: "OfferingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UniversityCountryID",
                table: "Students",
                column: "UniversityCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserID",
                table: "Students",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentTypeID",
                table: "Departments",
                column: "DepartmentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_DepartmentID",
                table: "Faculties",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_FacultyTypeID",
                table: "Faculties",
                column: "FacultyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_MaritalStatusTypeID",
                table: "Faculties",
                column: "MaritalStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_NationalityID",
                table: "Faculties",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_UserID",
                table: "Faculties",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_AcademicSemesterId",
                table: "Sections",
                column: "AcademicSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseId",
                table: "Sections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_FacultyId",
                table: "Sections",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SectionTypeId",
                table: "Sections",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMajorMinior_DepartmentMajorId",
                table: "StudentMajorMinior",
                column: "DepartmentMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMajorMinior_DepartmentMiniorId",
                table: "StudentMajorMinior",
                column: "DepartmentMiniorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMajorMinior_StudentId",
                table: "StudentMajorMinior",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_AcademicSemesterId",
                table: "StudentSections",
                column: "AcademicSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_CourseId",
                table: "StudentSections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_SectionId",
                table: "StudentSections",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StatusTypesId",
                table: "StudentSections",
                column: "StatusTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentId",
                table: "StudentSections",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_DepartmentTypes_DepartmentTypeID",
                table: "Departments",
                column: "DepartmentTypeID",
                principalTable: "DepartmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserID",
                table: "Students",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CountryTypes_UniversityCountryID",
                table: "Students",
                column: "UniversityCountryID",
                principalTable: "CountryTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_DepartmentDegrees_DepartmentDegreeID",
                table: "Students",
                column: "DepartmentDegreeID",
                principalTable: "DepartmentDegrees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OfferingTypes_OfferingTypeID",
                table: "Students",
                column: "OfferingTypeID",
                principalTable: "OfferingTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_DepartmentTypes_DepartmentTypeID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_CountryTypes_UniversityCountryID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_DepartmentDegrees_DepartmentDegreeID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_OfferingTypes_OfferingTypeID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "DepartmentTypes");

            migrationBuilder.DropTable(
                name: "StudentMajorMinior");

            migrationBuilder.DropTable(
                name: "StudentSections");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "FacultyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentDegreeID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_OfferingTypeID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UniversityCountryID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentTypeID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CountyOfHighSchoolAttended",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentDegreeID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EntryYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HighSchoolAttendedName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NameOfUniversity",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OfferingTypeID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Scholarship",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniversityCountryID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniversityEndYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniversityStartYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentTypeID",
                table: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
