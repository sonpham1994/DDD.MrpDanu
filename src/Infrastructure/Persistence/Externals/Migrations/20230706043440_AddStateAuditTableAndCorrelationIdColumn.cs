using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Externals.Migrations
{
    /// <inheritdoc />
    public partial class AddStateAuditTableAndCorrelationIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "AuditTable");

            migrationBuilder.AddColumn<string>(
                name: "CorrelationId",
                table: "AuditTable",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "StateAuditTableId",
                table: "AuditTable",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "StateAuditTable",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateAuditTable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTable_StateAuditTableId",
                table: "AuditTable",
                column: "StateAuditTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditTable_StateAuditTable_StateAuditTableId",
                table: "AuditTable",
                column: "StateAuditTableId",
                principalTable: "StateAuditTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditTable_StateAuditTable_StateAuditTableId",
                table: "AuditTable");

            migrationBuilder.DropTable(
                name: "StateAuditTable");

            migrationBuilder.DropIndex(
                name: "IX_AuditTable_StateAuditTableId",
                table: "AuditTable");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "AuditTable");

            migrationBuilder.DropColumn(
                name: "StateAuditTableId",
                table: "AuditTable");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AuditTable",
                type: "varchar(24)",
                nullable: false,
                defaultValue: "");
        }
    }
}
