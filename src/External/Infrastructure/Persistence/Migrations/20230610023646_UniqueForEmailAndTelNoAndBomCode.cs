using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UniqueForEmailAndTelNoAndBomCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_BoMRevision_Code",
                table: "BoMRevision",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoM_Code",
                table: "BoM",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactPersonInformation_Email",
                table: "ContactPersonInformation");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersonInformation_TelNo",
                table: "ContactPersonInformation");

            migrationBuilder.DropIndex(
                name: "IX_BoMRevision_Code",
                table: "BoMRevision");

            migrationBuilder.DropIndex(
                name: "IX_BoM_Code",
                table: "BoM");
        }
    }
}
