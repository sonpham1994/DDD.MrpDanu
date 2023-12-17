using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoMRevisionMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartner_TransactionalPartnerId",
                table: "BoMRevisionMaterial");

            migrationBuilder.RenameColumn(
                name: "TransactionalPartnerId",
                table: "BoMRevisionMaterial",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_BoMRevisionMaterial_TransactionalPartnerId",
                table: "BoMRevisionMaterial",
                newName: "IX_BoMRevisionMaterial_SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartner_SupplierId",
                table: "BoMRevisionMaterial",
                column: "SupplierId",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartner_SupplierId",
                table: "BoMRevisionMaterial");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "BoMRevisionMaterial",
                newName: "TransactionalPartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BoMRevisionMaterial_SupplierId",
                table: "BoMRevisionMaterial",
                newName: "IX_BoMRevisionMaterial_TransactionalPartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevisionMaterial_TransactionalPartner_TransactionalPartnerId",
                table: "BoMRevisionMaterial",
                column: "TransactionalPartnerId",
                principalTable: "TransactionalPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
