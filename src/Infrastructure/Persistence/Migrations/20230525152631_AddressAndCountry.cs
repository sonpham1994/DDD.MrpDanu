using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddressAndCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevision_BoM_BoMId",
                table: "BoMRevision");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "TransactionalPartners");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "TransactionalPartners",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNo",
                table: "TransactionalPartners",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "TransactionalPartners",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Ward",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "CountryId",
                table: "TransactionalPartners",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionalPartners_CountryId",
                table: "TransactionalPartners",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevision_BoM_BoMId",
                table: "BoMRevision",
                column: "BoMId",
                principalTable: "BoM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalPartners_Country_CountryId",
                table: "TransactionalPartners",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoMRevision_BoM_BoMId",
                table: "BoMRevision");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalPartners_Country_CountryId",
                table: "TransactionalPartners");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropIndex(
                name: "IX_TransactionalPartners_CountryId",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "Address_Ward",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "TransactionalPartners");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "TransactionalPartners");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNo",
                table: "TransactionalPartners",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "TransactionalPartners",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoMRevision_BoM_BoMId",
                table: "BoMRevision",
                column: "BoMId",
                principalTable: "BoM",
                principalColumn: "Id");
        }
    }
}
