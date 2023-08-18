using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class Initial10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountType",
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
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseCredits",
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
                    table.PrimaryKey("PK_CourseCredits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    CourseTypeID = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLab = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseTypes_CourseTypeID",
                        column: x => x.CourseTypeID,
                        principalTable: "CourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentBilingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    BillingTypeID = table.Column<int>(type: "int", nullable: true),
                    NationalCurrencyTypeID = table.Column<int>(type: "int", nullable: true),
                    NationalAmount = table.Column<float>(type: "real", nullable: false),
                    InternationalCurrencyTypeID = table.Column<int>(type: "int", nullable: true),
                    InternationalAmount = table.Column<float>(type: "real", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentBilingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentBilingItems_BillingTypes_BillingTypeID",
                        column: x => x.BillingTypeID,
                        principalTable: "BillingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentBilingItems_CurrencyTypes_InternationalCurrencyTypeID",
                        column: x => x.InternationalCurrencyTypeID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentBilingItems_CurrencyTypes_NationalCurrencyTypeID",
                        column: x => x.NationalCurrencyTypeID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentBilingItems_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Harmonize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harmonize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    StatusTypeID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentBills_CurrencyTypes_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentBills_StatusTypes_StatusTypeID",
                        column: x => x.StatusTypeID,
                        principalTable: "StatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentBills_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccounts_CurrencyTypes_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    AcademicID = table.Column<int>(type: "int", nullable: true),
                    CurrencyID = table.Column<int>(type: "int", nullable: true),
                    BankID = table.Column<int>(type: "int", nullable: false),
                    AccountTypeID = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunningBalance = table.Column<float>(type: "real", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAccounts_AcademicSemesters_AcademicID",
                        column: x => x.AcademicID,
                        principalTable: "AcademicSemesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAccounts_AccountType_AccountTypeID",
                        column: x => x.AccountTypeID,
                        principalTable: "AccountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAccounts_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAccounts_CurrencyTypes_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAccounts_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseBillingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    BillingTypeID = table.Column<int>(type: "int", nullable: true),
                    NationalCurrencyTypeID = table.Column<int>(type: "int", nullable: true),
                    NationalAmount = table.Column<float>(type: "real", nullable: false),
                    InternationalCurrencyTypeID = table.Column<int>(type: "int", nullable: true),
                    InternationalAmount = table.Column<float>(type: "real", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBillingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseBillingItems_BillingTypes_BillingTypeID",
                        column: x => x.BillingTypeID,
                        principalTable: "BillingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseBillingItems_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseBillingItems_CurrencyTypes_InternationalCurrencyTypeID",
                        column: x => x.InternationalCurrencyTypeID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseBillingItems_CurrencyTypes_NationalCurrencyTypeID",
                        column: x => x.NationalCurrencyTypeID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    LabCourseID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseBills_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseBills_Courses_LabCourseID",
                        column: x => x.LabCourseID,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    NationalAmount = table.Column<float>(type: "real", nullable: false),
                    InternationalAmount = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCost_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseCost_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoursePreRequisites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    PreRequisiteCourseID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePreRequisites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursePreRequisites_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoursePreRequisites_Courses_PreRequisiteCourseID",
                        column: x => x.PreRequisiteCourseID,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankID",
                table: "BankAccounts",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_CurrencyID",
                table: "BankAccounts",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBillingItems_BillingTypeID",
                table: "CourseBillingItems",
                column: "BillingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBillingItems_CourseID",
                table: "CourseBillingItems",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBillingItems_InternationalCurrencyTypeID",
                table: "CourseBillingItems",
                column: "InternationalCurrencyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBillingItems_NationalCurrencyTypeID",
                table: "CourseBillingItems",
                column: "NationalCurrencyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBills_CourseID",
                table: "CourseBills",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBills_LabCourseID",
                table: "CourseBills",
                column: "LabCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCost_CourseID",
                table: "CourseCost",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCost_DepartmentID",
                table: "CourseCost",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePreRequisites_CourseID",
                table: "CoursePreRequisites",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePreRequisites_PreRequisiteCourseID",
                table: "CoursePreRequisites",
                column: "PreRequisiteCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseTypeID",
                table: "Courses",
                column: "CourseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBilingItems_BillingTypeID",
                table: "DepartmentBilingItems",
                column: "BillingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBilingItems_DepartmentID",
                table: "DepartmentBilingItems",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBilingItems_InternationalCurrencyTypeID",
                table: "DepartmentBilingItems",
                column: "InternationalCurrencyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBilingItems_NationalCurrencyTypeID",
                table: "DepartmentBilingItems",
                column: "NationalCurrencyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_AcademicID",
                table: "StudentAccounts",
                column: "AcademicID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_AccountTypeID",
                table: "StudentAccounts",
                column: "AccountTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_BankID",
                table: "StudentAccounts",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_CurrencyID",
                table: "StudentAccounts",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_StudentID",
                table: "StudentAccounts",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBills_CurrencyID",
                table: "StudentBills",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBills_StatusTypeID",
                table: "StudentBills",
                column: "StatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBills_StudentID",
                table: "StudentBills",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CourseBillingItems");

            migrationBuilder.DropTable(
                name: "CourseBills");

            migrationBuilder.DropTable(
                name: "CourseCost");

            migrationBuilder.DropTable(
                name: "CourseCredits");

            migrationBuilder.DropTable(
                name: "CoursePreRequisites");

            migrationBuilder.DropTable(
                name: "DepartmentBilingItems");

            migrationBuilder.DropTable(
                name: "Harmonize");

            migrationBuilder.DropTable(
                name: "StudentAccounts");

            migrationBuilder.DropTable(
                name: "StudentBills");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
