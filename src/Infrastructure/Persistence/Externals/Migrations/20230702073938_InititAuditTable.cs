using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Externals.Migrations
{
    /// <inheritdoc />
    public partial class InititAuditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectId = table.Column<string>(type: "varchar(250)", nullable: false),
                    ObjectName = table.Column<string>(type: "varchar(50)", nullable: false),
                    TimeGenerated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<string>(type: "varchar(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTable");
        }
    }
}
