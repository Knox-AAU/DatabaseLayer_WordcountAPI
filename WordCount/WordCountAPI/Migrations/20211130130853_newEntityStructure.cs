using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WordCount.Migrations
{
    public partial class newEntityStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Term_Article_ArticleId",
                table: "Term");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Term",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Term");

            migrationBuilder.RenameColumn(
                name: "Word",
                table: "Term",
                newName: "Literal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Term",
                table: "Term",
                column: "Literal");

            migrationBuilder.CreateTable(
                name: "WordOccurance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArticleId = table.Column<long>(type: "bigint", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    WordLiteral = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordOccurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordOccurance_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordOccurance_Term_WordLiteral",
                        column: x => x.WordLiteral,
                        principalTable: "Term",
                        principalColumn: "Literal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordOccurance_ArticleId",
                table: "WordOccurance",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_WordOccurance_WordLiteral",
                table: "WordOccurance",
                column: "WordLiteral");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordOccurance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Term",
                table: "Term");

            migrationBuilder.RenameColumn(
                name: "Literal",
                table: "Term",
                newName: "Word");

            migrationBuilder.AddColumn<long>(
                name: "ArticleId",
                table: "Term",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Term",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Term",
                table: "Term",
                columns: new[] { "ArticleId", "Word" });

            migrationBuilder.AddForeignKey(
                name: "FK_Term_Article_ArticleId",
                table: "Term",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
