using System;
using Microsoft.EntityFrameworkCore;
using WordCount.Models;

namespace WordCount.Data
{
    public sealed class WordListContext : DbContext
    {
        public DbSet<WordListModel> Wordlist { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = Environment.GetEnvironmentVariable("database:connectionString");
            
            if (connectionString == null)
            {
                throw new Exception("Connection string not found.");
            }
            
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("citext");
        }
    }
}