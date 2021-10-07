using System;
using Microsoft.EntityFrameworkCore;
using WordCount.Models;

namespace WordCount.Data
{
    public sealed class WordCountDbContext : DbContext
    {
        public DbSet<WordListModel> Wordlist { get; set; }
        public DbSet<AppearsInModel> AppearsIn { get; set; }
        public DbSet<ExternalSourcesModel> ExternalSources { get; set; }
        public DbSet<FileListModel> FileList { get; set; }

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