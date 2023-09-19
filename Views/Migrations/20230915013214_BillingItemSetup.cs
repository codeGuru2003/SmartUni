using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUni.Migrations
{
    public partial class BillingItemSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeID = table.Column<int>(type: "int", nullable: true),
                    BillingTypeID = table.Column<int>(type: "int", nullable: true),
                    CurrencyTypeID = table.Column<int>(type: "int", nullable: true),
                    CurrencyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingItems_BillingTypes_BillingTypeID",
                        column: x => x.BillingTypeID,
                        principalTable: "BillingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillingItems_CollegeTypes_CollegeID",
                        column: x => x.CollegeID,
                        principalTable: "CollegeTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillingItems_CurrencyTypes_CurrencyTypeID",
                        column: x => x.CurrencyTypeID,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingItems_BillingTypeID",
                table: "BillingItems",
                column: "BillingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BillingItems_CollegeID",
                table: "BillingItems",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_BillingItems_CurrencyTypeID",
                table: "BillingItems",
                column: "CurrencyTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingItems");
        }
    }
}
