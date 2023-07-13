using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueForEmailAndTelNoInContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactPersonInformation_Email",
                table: "ContactPersonInformation");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersonInformation_TelNo",
                table: "ContactPersonInformation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonInformation_Email",
                table: "ContactPersonInformation",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonInformation_TelNo",
                table: "ContactPersonInformation",
                column: "TelNo",
                unique: true,
                filter: "[TelNo] IS NOT NULL");
        }
    }
}
