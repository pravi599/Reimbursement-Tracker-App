using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReimbursementTrackerApp.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Requests_RequestID",
                table: "PaymentDetails");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "PaymentDetails",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "PaymentDetails",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_RequestID",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_RequestId");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "PaymentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Requests_RequestId",
                table: "PaymentDetails",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Requests_RequestId",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "PaymentDetails");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "PaymentDetails",
                newName: "RequestID");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "PaymentDetails",
                newName: "PaymentID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_RequestId",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_RequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Requests_RequestID",
                table: "PaymentDetails",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
