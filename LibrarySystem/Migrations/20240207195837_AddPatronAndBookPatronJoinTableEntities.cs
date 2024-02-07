using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    public partial class AddPatronAndBookPatronJoinTableEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patrons",
                columns: table => new
                {
                    PatronId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatronName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LibraryCard = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrons", x => x.PatronId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookPatrons",
                columns: table => new
                {
                    BookPatronId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PatronId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPatrons", x => x.BookPatronId);
                    table.ForeignKey(
                        name: "FK_BookPatrons_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPatrons_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "PatronId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BookPatrons_BookId",
                table: "BookPatrons",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPatrons_PatronId",
                table: "BookPatrons",
                column: "PatronId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPatrons");

            migrationBuilder.DropTable(
                name: "Patrons");
        }
    }
}
