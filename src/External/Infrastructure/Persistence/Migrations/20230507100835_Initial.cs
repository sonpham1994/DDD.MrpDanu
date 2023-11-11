using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "bomrevisionseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "productseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CurrencyType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionalMarket",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalMarket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionalPartnerType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionalPartnerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "char(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoM_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Varian = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CodeUnique = table.Column<string>(type: "varchar(2000)", nullable: false),
                    MaterialTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    RegionalMarketId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialType_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materials_RegionalMarket_RegionalMarketId",
                        column: x => x.RegionalMarketId,
                        principalTable: "RegionalMarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionalPartners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    TaxNo = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TelNo = table.Column<string>(type: "varchar(50)", nullable: false),
                    ContactPersonName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LocationTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    TransactionalPartnerTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    CurrencyTypeId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionalPartners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionalPartners_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "CurrencyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionalPartners_LocationType_LocationTypeId",
                        column: x => x.LocationTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionalPartners_TransactionalPartnerType_TransactionalPartnerTypeId",
                        column: x => x.TransactionalPartnerTypeId,
                        principalTable: "TransactionalPartnerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoMRevision",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    Code = table.Column<string>(type: "char(14)", nullable: false),
                    Confirmation = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    BoMId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoMRevision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoMRevision_BoM_BoMId",
                        column: x => x.BoMId,
                        principalTable: "BoM",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialCostManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Surcharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionalPartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCostManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialCostManagement_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialCostManagement_TransactionalPartners_TransactionalPartnerId",
                        column: x => x.TransactionalPartnerId,
                        principalTable: "TransactionalPartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoMRevisionMaterial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionalPartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoMRevisionId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoMRevisionMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoMRevisionMaterial_BoMRevision_BoMRevisionId",
                        column: x => x.BoMRevisionId,
                        principalTable: "BoMRevision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoMRevisionMaterial_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoMRevisionMaterial_TransactionalPartners_TransactionalPartnerId",
                        column: x => x.TransactionalPartnerId,
                        principalTable: "TransactionalPartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoMRevision_BoMId",
                table: "BoMRevision",
                column: "BoMId");

            migrationBuilder.CreateIndex(
                name: "IX_BoMRevisionMaterial_BoMRevisionId",
                table: "BoMRevisionMaterial",
                column: "BoMRevisionId");

            migrationBuilder.CreateIndex(
                name: "IX_BoMRevisionMaterial_MaterialId",
                table: "BoMRevisionMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_BoMRevisionMaterial_TransactionalPartnerId",
                table: "BoMRevisionMaterial",
                column: "TransactionalPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCostManagement_MaterialId",
                table: "MaterialCostManagement",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCostManagement_TransactionalPartnerId",
                table: "MaterialCostManagement",
                column: "TransactionalPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_RegionalMarketId",
                table: "Materials",
                column: "RegionalMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionalPartners_CurrencyTypeId",
                table: "TransactionalPartners",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionalPartners_LocationTypeId",
                table: "TransactionalPartners",
                column: "LocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionalPartners_TransactionalPartnerTypeId",
                table: "TransactionalPartners",
                column: "TransactionalPartnerTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoMRevisionMaterial");

            migrationBuilder.DropTable(
                name: "MaterialCostManagement");

            migrationBuilder.DropTable(
                name: "BoMRevision");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "TransactionalPartners");

            migrationBuilder.DropTable(
                name: "BoM");

            migrationBuilder.DropTable(
                name: "MaterialType");

            migrationBuilder.DropTable(
                name: "RegionalMarket");

            migrationBuilder.DropTable(
                name: "CurrencyType");

            migrationBuilder.DropTable(
                name: "LocationType");

            migrationBuilder.DropTable(
                name: "TransactionalPartnerType");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropSequence(
                name: "bomrevisionseq");

            migrationBuilder.DropSequence(
                name: "productseq");
        }
    }
}
