using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
            modelBuilder.Entity<Term>().HasKey(a => a.Literal);
            modelBuilder.Entity<Article>().HasMany<WordOccurance>(a => a.Words).WithOne(w => w.Article);
            modelBuilder.Entity<WordOccurance>().HasKey(occurance => occurance.Id);
            modelBuilder
                .Entity<WordRatio>()
                .ToView(nameof(WordRatio))
                .HasNoKey();
        }
    }


    public class ArticleContextOld : DbContext
    {
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Article> Articles { get; set; }
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
