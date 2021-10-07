using System;
using Microsoft.EntityFrameworkCore;
using WordCount.Models;

namespace WordCount.Data
{
    public class WordCountContext : DbContext
    {
        public DbSet<WordList> wordlist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = Environment.GetEnvironmentVariable("database:connectionString");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("citext");
        }
    }
}