using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactPersonInforWithPkToPk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_TransactionalPartnerId",
                table: "ContactPersonInformation");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersonInformation_TransactionalPartnerId",
                table: "ContactPersonInformation");

            migrationBuilder.DropColumn(
                name: "TransactionalPartnerId",
                table: "ContactPersonInformation");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_Id",
                table: "ContactPersonInformation",
                column: "Id",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_Id",
                table: "ContactPersonInformation");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionalPartnerId",
                table: "ContactPersonInformation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonInformation_TransactionalPartnerId",
                table: "ContactPersonInformation",
                column: "TransactionalPartnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_TransactionalPartnerId",
                table: "ContactPersonInformation",
                column: "TransactionalPartnerId",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
