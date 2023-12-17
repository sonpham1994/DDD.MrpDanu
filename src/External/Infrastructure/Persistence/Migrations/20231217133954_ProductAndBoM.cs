using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductAndBoM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoM_Product_Id",
                table: "BoM");

            migrationBuilder.AddColumn<long>(
                name: "BoMId",
                table: "Product",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "BoM",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BoMId",
                table: "Product",
                column: "BoMId",
                unique: true,
                filter: "[BoMId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Code",
                table: "Material",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoM_ProductId",
                table: "BoM",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoM_Product_ProductId",
                table: "BoM",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_BoM_BoMId",
                table: "Product",
                column: "BoMId",
                principalTable: "BoM",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoM_Product_ProductId",
                table: "BoM");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_BoM_BoMId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BoMId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Material_Code",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_BoM_ProductId",
                table: "BoM");

            migrationBuilder.DropColumn(
                name: "BoMId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BoM");

            migrationBuilder.AddForeignKey(
                name: "FK_BoM_Product_Id",
                table: "BoM",
                column: "Id",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
