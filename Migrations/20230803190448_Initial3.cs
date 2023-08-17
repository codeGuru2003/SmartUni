using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EntranceApplicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    TitleID = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderID = table.Column<int>(type: "int", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hometown_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalityID = table.Column<int>(type: "int", nullable: true),
                    CountryID = table.Column<int>(type: "int", nullable: true),
                    ReligionID = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusID = table.Column<int>(type: "int", nullable: false),
                    NumberofChildren = table.Column<int>(type: "int", nullable: true),
                    CurrentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextofKin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationID = table.Column<int>(type: "int", nullable: true),
                    EmergencyContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipTypeID = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisabilityTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceApplicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_CountryTypes_CountryID",
                        column: x => x.CountryID,
                        principalTable: "CountryTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_DisabilityTypes_DisabilityTypeID",
                        column: x => x.DisabilityTypeID,
                        principalTable: "DisabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_MaritalStatusTypes_MaritalStatusID",
                        column: x => x.MaritalStatusID,
                        principalTable: "MaritalStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_NationalityTypes_NationalityID",
                        column: x => x.NationalityID,
                        principalTable: "NationalityTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_OccupationTypes_OccupationID",
                        column: x => x.OccupationID,
                        principalTable: "OccupationTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_RelationshipTypes_RelationshipTypeID",
                        column: x => x.RelationshipTypeID,
                        principalTable: "RelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_ReligionTypes_ReligionID",
                        column: x => x.ReligionID,
                        principalTable: "ReligionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_StatusTypes_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntranceApplicants_TitleTypes_TitleID",
                        column: x => x.TitleID,
                        principalTable: "TitleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StatusID",
                table: "Students",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_CountryID",
                table: "EntranceApplicants",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_DisabilityTypeID",
                table: "EntranceApplicants",
                column: "DisabilityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_GenderID",
                table: "EntranceApplicants",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_MaritalStatusID",
                table: "EntranceApplicants",
                column: "MaritalStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_NationalityID",
                table: "EntranceApplicants",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_OccupationID",
                table: "EntranceApplicants",
                column: "OccupationID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_RelationshipTypeID",
                table: "EntranceApplicants",
                column: "RelationshipTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_ReligionID",
                table: "EntranceApplicants",
                column: "ReligionID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_StatusID",
                table: "EntranceApplicants",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceApplicants_TitleID",
                table: "EntranceApplicants",
                column: "TitleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StatusTypes_StatusID",
                table: "Students",
                column: "StatusID",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StatusTypes_StatusID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "EntranceApplicants");

            migrationBuilder.DropIndex(
                name: "IX_Students_StatusID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Students");
        }
    }
}
