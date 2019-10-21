using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace musicList2.Migrations
{
    public partial class AppDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    ListGUID = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    KeywordHash = table.Column<string>(nullable: true),
                    MasterKeyHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.GUID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Lists");
        }
    }
}
