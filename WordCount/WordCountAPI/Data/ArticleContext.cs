using Microsoft.EntityFrameworkCore;
using WordCount.Data.Models;

namespace WordCount.Data
{
    public class ArticleContext : DbContext
    {
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<JsonSchemaModel> JsonSchemas { get; set; }
        public DbSet<WordRatio> WordRatios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("database_connectionString"));
            optionsBuilder.UseNpgsql(
                "UserID=postgres;Password=Sysadmins.;Host=localhost;Port=5002;Database=WordCount_DB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasKey(a => a.Literal);
            modelBuilder.Entity<Article>().HasMany(a => a.WordOccurrences).WithOne(w => w.Article);
            modelBuilder.Entity<WordOccurrence>().HasKey(occurance => new { occurance.ArticleId, occurance.Word.Literal });
            modelBuilder
                .Entity<WordRatio>()
                .ToView(nameof(WordRatio))
                .HasNoKey();
        }
    }

    public class ArticleContextOld : DbContext
    {
        public DbSet<Publisher_old> Publishers { get; set; }
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
            modelBuilder.Entity<Term>().HasKey(a => new { a.ArticleId, a.Word });
            modelBuilder
                .Entity<WordRatio>()
                .ToView(nameof(WordRatio))
                .HasNoKey();
        }
    }
}
