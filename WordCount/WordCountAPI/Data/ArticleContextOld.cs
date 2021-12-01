using Microsoft.EntityFrameworkCore;
using WordCount.Data.Models;

namespace WordCount.Data
{
    public class ArticleContextOld : DbContext
    {
        public DbSet<Publisher_old> Publishers { get; set; }
        public DbSet<Term_old> Terms { get; set; }
        public DbSet<Article_old> Articles { get; set; }
        public DbSet<JsonSchemaModel> JsonSchemas { get; set; }
        public DbSet<WordRatio> WordRatios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "UserID=postgres;Password=Sysadmins.;Host=localhost;Port=5002;Database=wordcount");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Term_old>().HasKey(a => new { a.ArticleId, a.Word });
            modelBuilder
                .Entity<WordRatio>()
                .ToView(nameof(WordRatio))
                .HasNoKey();
        }
    }
}