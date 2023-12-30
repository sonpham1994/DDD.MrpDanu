using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDomainModelForMaterialAndProductionPlanning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPersonName",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "TelNo",
                table: "TransactionalPartners");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "TransactionalPartners",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNo",
                table: "TransactionalPartners",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "CurrencyTypeId",
                table: "MaterialCostManagement",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "CurrencyTypeId",
                table: "BoMRevisionMaterial",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "ContactPersonInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TelNo = table.Column<string>(type: "varchar(50)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPersonInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPersonInformation_TransactionalPartners_Id",
                        column: x => x.Id,
                        principalTable: "TransactionalPartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCostManagement_CurrencyTypeId",
                table: "MaterialCostManagement",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoMRevisionMaterial_CurrencyTypeId",
                table: "BoMRevisionMaterial",
                column: "CurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevisionMaterial_CurrencyType_CurrencyTypeId",
                table: "BoMRevisionMaterial",
                column: "CurrencyTypeId",
                principalTable: "CurrencyType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCostManagement_CurrencyType_CurrencyTypeId",
                table: "MaterialCostManagement",
                column: "CurrencyTypeId",
                principalTable: "CurrencyType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevisionMaterial_CurrencyType_CurrencyTypeId",
                table: "BoMRevisionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCostManagement_CurrencyType_CurrencyTypeId",
                table: "MaterialCostManagement");

            migrationBuilder.DropTable(
                name: "ContactPersonInformation");

            migrationBuilder.DropIndex(
                name: "IX_MaterialCostManagement_CurrencyTypeId",
                table: "MaterialCostManagement");

            migrationBuilder.DropIndex(
                name: "IX_BoMRevisionMaterial_CurrencyTypeId",
                table: "BoMRevisionMaterial");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                table: "MaterialCostManagement");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                table: "BoMRevisionMaterial");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "TransactionalPartners",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxNo",
                table: "TransactionalPartners",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonName",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelNo",
                table: "TransactionalPartners",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
