using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace telehealth.Migrations
{
    public partial class Test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Prescriptions_PrescriptionId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_PrescriptionId",
                table: "MedicalRecords");

            migrationBuilder.AddColumn<int>(
                name: "HelpId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Helps",
                columns: table => new
                {
                    HelpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestorId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helps", x => x.HelpId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HelpId",
                table: "Comments",
                column: "HelpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Helps_HelpId",
                table: "Comments",
                column: "HelpId",
                principalTable: "Helps",
                principalColumn: "HelpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Helps_HelpId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Helps");

            migrationBuilder.DropIndex(
                name: "IX_Comments_HelpId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "HelpId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PrescriptionId",
                table: "MedicalRecords",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Prescriptions_PrescriptionId",
                table: "MedicalRecords",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
