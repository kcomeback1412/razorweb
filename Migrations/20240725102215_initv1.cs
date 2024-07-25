using System;
using Bogus;
using CS58_Razor09EF.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS58_Razor09EF.Migrations
{
    /// <inheritdoc />
    public partial class initv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ID);
                });

            Randomizer.Seed = new Random(8675309);
            var fakerArticle = new Faker<Article>();
            fakerArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5, 5));
            fakerArticle.RuleFor(a => a.PublishDate, f => f.Date.Between(new DateTime(2021,1,1), new DateTime(2024,7,7)));
            fakerArticle.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1,4));

            for (int i = 0; i < 100; i++)
            {
                Article article = fakerArticle.Generate();
                migrationBuilder.InsertData(
                    table: "Articles",
                    columns: new[] { "Title" , "PublishDate", "Content" },
                    values: new object[]
                    {
                        article.Title,
                        article.PublishDate,
                        article.Content
                    }
					);
            }
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
