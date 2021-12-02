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
        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("UserID=postgres;Password=Sysadmins.;Host=localhost;Port=5002;Database=wordcount;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HasWord>().HasKey(a => a.Id);
            modelBuilder
                .Entity<WordRatio>()
                .ToView(nameof(WordRatio))
                .HasNoKey();
        }
    }
}