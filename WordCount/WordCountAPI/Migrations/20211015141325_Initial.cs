using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WordCount.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    TotalWordsInArticle = table.Column<int>(type: "integer", nullable: false),
                    PublisherName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Publishers_PublisherName",
                        column: x => x.PublisherName,
                        principalTable: "Publishers",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WordOccurances",
                columns: table => new
                {
                    WordOccuranceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArticleId = table.Column<long>(type: "bigint", nullable: true),
                    Occurances = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordOccurances", x => x.WordOccuranceId);
                    table.ForeignKey(
                        name: "FK_WordOccurances_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Literal = table.Column<string>(type: "text", nullable: false),
                    WordOccurancesWordOccuranceId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Literal);
                    table.ForeignKey(
                        name: "FK_Words_WordOccurances_WordOccurancesWordOccuranceId",
                        column: x => x.WordOccurancesWordOccuranceId,
                        principalTable: "WordOccurances",
                        principalColumn: "WordOccuranceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublisherName",
                table: "Articles",
                column: "PublisherName");

            migrationBuilder.CreateIndex(
                name: "IX_WordOccurances_ArticleId",
                table: "WordOccurances",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordOccurancesWordOccuranceId",
                table: "Words",
                column: "WordOccurancesWordOccuranceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "WordOccurances");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
