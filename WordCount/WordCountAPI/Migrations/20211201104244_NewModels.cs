using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WordCount.Migrations
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JsonSchema",
                columns: table => new
                {
                    SchemaName = table.Column<string>(type: "text", nullable: false),
                    JsonString = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonSchema", x => x.SchemaName);
                });

            migrationBuilder.CreateTable(
                name: "new_publisher",
                columns: table => new
                {
                    PublisherName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_new_publisher", x => x.PublisherName);
                });

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Literal = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Literal);
                });

            migrationBuilder.CreateTable(
                name: "new_article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    TotalWords = table.Column<int>(type: "integer", nullable: false),
                    PublisherName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_new_article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_new_article_new_publisher_PublisherName",
                        column: x => x.PublisherName,
                        principalTable: "new_publisher",
                        principalColumn: "PublisherName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WordOccurrence",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    WordLiteral = table.Column<string>(type: "text", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordOccurrence_new_article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "new_article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordOccurrence_Word_WordLiteral",
                        column: x => x.WordLiteral,
                        principalTable: "Word",
                        principalColumn: "Literal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_new_article_PublisherName",
                table: "new_article",
                column: "PublisherName");

            migrationBuilder.CreateIndex(
                name: "IX_new_publisher_PublisherName",
                table: "new_publisher",
                column: "PublisherName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordOccurrence_ArticleId",
                table: "WordOccurrence",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_WordOccurrence_WordLiteral",
                table: "WordOccurrence",
                column: "WordLiteral");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JsonSchema");

            migrationBuilder.DropTable(
                name: "WordOccurrence");

            migrationBuilder.DropTable(
                name: "new_article");

            migrationBuilder.DropTable(
                name: "Word");

            migrationBuilder.DropTable(
                name: "new_publisher");
        }
    }
}
