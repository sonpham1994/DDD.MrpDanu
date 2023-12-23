using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorToStronglyTypedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_Id",
                table: "ContactPersonInformation");

            migrationBuilder.DropTable(
                name: "MaterialCostManagement");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "BoMRevision",
                newName: "Revision");

            migrationBuilder.RenameIndex(
                name: "IX_BoMRevision_Code",
                table: "BoMRevision",
                newName: "IX_BoMRevision_Revision");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "BoM",
                newName: "Revision");

            migrationBuilder.RenameIndex(
                name: "IX_BoM_Code",
                table: "BoM",
                newName: "IX_BoM_Revision");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionalPartnerId",
                table: "ContactPersonInformation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MaterialSupplierCost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Surcharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialSupplierCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialSupplierCost_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialSupplierCost_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialSupplierCost_TransactionalPartner_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "TransactionalPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonInformation_TransactionalPartnerId",
                table: "ContactPersonInformation",
                column: "TransactionalPartnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialSupplierCost_CurrencyTypeId",
                table: "MaterialSupplierCost",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialSupplierCost_MaterialId",
                table: "MaterialSupplierCost",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialSupplierCost_SupplierId",
                table: "MaterialSupplierCost",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_TransactionalPartnerId",
                table: "ContactPersonInformation",
                column: "TransactionalPartnerId",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_TransactionalPartnerId",
                table: "ContactPersonInformation");

            migrationBuilder.DropTable(
                name: "MaterialSupplierCost");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersonInformation_TransactionalPartnerId",
                table: "ContactPersonInformation");

            migrationBuilder.DropColumn(
                name: "TransactionalPartnerId",
                table: "ContactPersonInformation");

            migrationBuilder.RenameColumn(
                name: "Revision",
                table: "BoMRevision",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_BoMRevision_Revision",
                table: "BoMRevision",
                newName: "IX_BoMRevision_Code");

            migrationBuilder.RenameColumn(
                name: "Revision",
                table: "BoM",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_BoM_Revision",
                table: "BoM",
                newName: "IX_BoM_Code");

            migrationBuilder.CreateTable(
                name: "MaterialCostManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionalPartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    CurrencyTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Surcharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCostManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialCostManagement_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialCostManagement_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialCostManagement_TransactionalPartner_TransactionalPartnerId",
                        column: x => x.TransactionalPartnerId,
                        principalTable: "TransactionalPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCostManagement_CurrencyTypeId",
                table: "MaterialCostManagement",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCostManagement_MaterialId",
                table: "MaterialCostManagement",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCostManagement_TransactionalPartnerId",
                table: "MaterialCostManagement",
                column: "TransactionalPartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_Id",
                table: "ContactPersonInformation",
                column: "Id",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
