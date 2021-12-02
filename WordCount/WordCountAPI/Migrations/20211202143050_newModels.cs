using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WordCount.Migrations
{
    public partial class newModels : Migration
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
                name: "Word",
                columns: table => new
                {
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Text);
                });

            migrationBuilder.CreateTable(
                name: "Article",
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
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Publisher_PublisherName",
                        column: x => x.PublisherName,
                        principalTable: "Publisher",
                        principalColumn: "PublisherName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HasWord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    WordText = table.Column<string>(type: "text", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HasWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HasWord_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HasWord_Word_WordText",
                        column: x => x.WordText,
                        principalTable: "Word",
                        principalColumn: "Text",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_PublisherName",
                table: "Article",
                column: "PublisherName");

            migrationBuilder.CreateIndex(
                name: "IX_HasWord_ArticleId",
                table: "HasWord",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_HasWord_WordText",
                table: "HasWord",
                column: "WordText");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_PublisherName",
                table: "Publisher",
                column: "PublisherName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HasWord");

            migrationBuilder.DropTable(
                name: "JsonSchema");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Word");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
