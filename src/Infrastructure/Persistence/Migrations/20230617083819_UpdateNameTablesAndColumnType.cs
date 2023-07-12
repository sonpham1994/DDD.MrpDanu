using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTablesAndColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoM_Products_Id",
                table: "BoM");

            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevisionMaterial_Materials_MaterialId",
                table: "BoMRevisionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartners_TransactionalPartnerId",
                table: "BoMRevisionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartners_Id",
                table: "ContactPersonInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCostManagement_Materials_MaterialId",
                table: "MaterialCostManagement");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCostManagement_TransactionalPartners_TransactionalPartnerId",
                table: "MaterialCostManagement");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialType_MaterialTypeId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_RegionalMarket_RegionalMarketId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartners_Country_CountryId",
                table: "TransactionalPartners");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartners_CurrencyType_CurrencyTypeId",
                table: "TransactionalPartners");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartners_LocationType_LocationTypeId",
                table: "TransactionalPartners");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartners_TransactionalPartnerType_TransactionalPartnerTypeId",
                table: "TransactionalPartners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionalPartners",
                table: "TransactionalPartners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.RenameTable(
                name: "TransactionalPartners",
                newName: "TransactionalPartner");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartners_TransactionalPartnerTypeId",
                table: "TransactionalPartner",
                newName: "IX_TransactionalPartner_TransactionalPartnerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartners_LocationTypeId",
                table: "TransactionalPartner",
                newName: "IX_TransactionalPartner_LocationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartners_CurrencyTypeId",
                table: "TransactionalPartner",
                newName: "IX_TransactionalPartner_CurrencyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartners_CountryId",
                table: "TransactionalPartner",
                newName: "IX_TransactionalPartner_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_RegionalMarketId",
                table: "Material",
                newName: "IX_Material_RegionalMarketId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_MaterialTypeId",
                table: "Material",
                newName: "IX_Material_MaterialTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionalPartner",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionalPartner",
                table: "TransactionalPartner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoM_Product_Id",
                table: "BoM",
                column: "Id",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevisionMaterial_Material_MaterialId",
                table: "BoMRevisionMaterial",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartner_TransactionalPartnerId",
                table: "BoMRevisionMaterial",
                column: "TransactionalPartnerId",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_Id",
                table: "ContactPersonInformation",
                column: "Id",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_MaterialType_MaterialTypeId",
                table: "Material",
                column: "MaterialTypeId",
                principalTable: "MaterialType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_RegionalMarket_RegionalMarketId",
                table: "Material",
                column: "RegionalMarketId",
                principalTable: "RegionalMarket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCostManagement_Material_MaterialId",
                table: "MaterialCostManagement",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCostManagement_TransactionalPartner_TransactionalPartnerId",
                table: "MaterialCostManagement",
                column: "TransactionalPartnerId",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartner_Country_CountryId",
                table: "TransactionalPartner",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartner_CurrencyType_CurrencyTypeId",
                table: "TransactionalPartner",
                column: "CurrencyTypeId",
                principalTable: "CurrencyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartner_LocationType_LocationTypeId",
                table: "TransactionalPartner",
                column: "LocationTypeId",
                principalTable: "LocationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartner_TransactionalPartnerType_TransactionalPartnerTypeId",
                table: "TransactionalPartner",
                column: "TransactionalPartnerTypeId",
                principalTable: "TransactionalPartnerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoM_Product_Id",
                table: "BoM");

            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevisionMaterial_Material_MaterialId",
                table: "BoMRevisionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartner_TransactionalPartnerId",
                table: "BoMRevisionMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartner_Id",
                table: "ContactPersonInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_MaterialType_MaterialTypeId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_RegionalMarket_RegionalMarketId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCostManagement_Material_MaterialId",
                table: "MaterialCostManagement");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCostManagement_TransactionalPartner_TransactionalPartnerId",
                table: "MaterialCostManagement");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartner_Country_CountryId",
                table: "TransactionalPartner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartner_CurrencyType_CurrencyTypeId",
                table: "TransactionalPartner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartner_LocationType_LocationTypeId",
                table: "TransactionalPartner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartner_TransactionalPartnerType_TransactionalPartnerTypeId",
                table: "TransactionalPartner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionalPartner",
                table: "TransactionalPartner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.RenameTable(
                name: "TransactionalPartner",
                newName: "TransactionalPartners");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartner_TransactionalPartnerTypeId",
                table: "TransactionalPartners",
                newName: "IX_TransactionalPartners_TransactionalPartnerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartner_LocationTypeId",
                table: "TransactionalPartners",
                newName: "IX_TransactionalPartners_LocationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartner_CurrencyTypeId",
                table: "TransactionalPartners",
                newName: "IX_TransactionalPartners_CurrencyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalPartner_CountryId",
                table: "TransactionalPartners",
                newName: "IX_TransactionalPartners_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_RegionalMarketId",
                table: "Materials",
                newName: "IX_Materials_RegionalMarketId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_MaterialTypeId",
                table: "Materials",
                newName: "IX_Materials_MaterialTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionalPartners",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionalPartners",
                table: "TransactionalPartners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoM_Products_Id",
                table: "BoM",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevisionMaterial_Materials_MaterialId",
                table: "BoMRevisionMaterial",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartners_TransactionalPartnerId",
                table: "BoMRevisionMaterial",
                column: "TransactionalPartnerId",
                principalTable: "TransactionalPartners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersonInformation_TransactionalPartners_Id",
                table: "ContactPersonInformation",
                column: "Id",
                principalTable: "TransactionalPartners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCostManagement_Materials_MaterialId",
                table: "MaterialCostManagement",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCostManagement_TransactionalPartners_TransactionalPartnerId",
                table: "MaterialCostManagement",
                column: "TransactionalPartnerId",
                principalTable: "TransactionalPartners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialType_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId",
                principalTable: "MaterialType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_RegionalMarket_RegionalMarketId",
                table: "Materials",
                column: "RegionalMarketId",
                principalTable: "RegionalMarket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartners_Country_CountryId",
                table: "TransactionalPartners",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartners_CurrencyType_CurrencyTypeId",
                table: "TransactionalPartners",
                column: "CurrencyTypeId",
                principalTable: "CurrencyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartners_LocationType_LocationTypeId",
                table: "TransactionalPartners",
                column: "LocationTypeId",
                principalTable: "LocationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartners_TransactionalPartnerType_TransactionalPartnerTypeId",
                table: "TransactionalPartners",
                column: "TransactionalPartnerTypeId",
                principalTable: "TransactionalPartnerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
