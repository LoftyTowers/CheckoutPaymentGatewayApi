using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class InitialPaymentDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVC = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentStatus_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "PaymentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentStatus",
                columns: new[] { "Id", "StatusDesc" },
                values: new object[,]
                {
                    { 0, "Unknown" },
                    { 10, "RequestRecieved" },
                    { 20, "RequestSent" },
                    { 30, "RequestSucceded" },
                    { 999, "RequestFailed" },
                    { 1009, "DuplicateRequest" },
                    { 1019, "RequestDoesNotExist" },
                    { 1029, "InsuffucentFunds" },
                    { 1039, "CardNotActivated" },
                    { 1049, "StolenCancelled" },
                    { 1059, "InvalidCardCredentials" },
                    { 1069, "CardExpired" },
                    { 1100, "PaymentNotStored" },
                    { 9999, "Error" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CardId",
                table: "Payment",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentStatusId",
                table: "Payment",
                column: "PaymentStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
