using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
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
                    JsonString = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonSchema", x => x.SchemaName);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.PublisherName);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Literal = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Literal);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TotalWords = table.Column<int>(type: "integer", nullable: false),
                    PublisherName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Publisher_PublisherName",
                        column: x => x.PublisherName,
                        principalTable: "Publisher",
                        principalColumn: "PublisherName",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_WordOccurrence_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordOccurrence_Words_WordLiteral",
                        column: x => x.WordLiteral,
                        principalTable: "Words",
                        principalColumn: "Literal");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_PublisherName",
                table: "Article",
                column: "PublisherName");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_PublisherName",
                table: "Publisher",
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
                name: "Article");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
