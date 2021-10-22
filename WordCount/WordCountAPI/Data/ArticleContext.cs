using System;
using Code_first_test.Models;
using Microsoft.EntityFrameworkCore;

namespace WordCount.Data
{
    public class ArticleContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<OccursIn> WordOccurances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
            string? connectionString = Environment.GetEnvironmentVariable("database_connectionString");
            
            if (connectionString == null)
            {
                throw new Exception("Connection string not found.");
            }
            */
            
            optionsBuilder.UseNpgsql("Host=localhost;UserID=postgres;Password=Sysadmins.;Port=5432;Database=wordcount;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}