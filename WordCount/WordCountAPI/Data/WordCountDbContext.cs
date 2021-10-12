using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WordCount.Models;

namespace WordCount.Data
{
    /// <summary>
    /// Implementation of DbContext responsible for establishing a connection to the database and configuring the
    /// models on the C# side to match the attributes of the database relations.
    /// </summary>
    public sealed class WordCountDbContext : DbContext
    {
        public DbSet<WordListModel> Wordlist { get; set; }
        public DbSet<AppearsInModel> AppearsIn { get; set; }
        public DbSet<ExternalSourcesModel> ExternalSources { get; set; }
        public DbSet<FileListModel> FileList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine($"{environmentVariable.Key}: {environmentVariable.Value}");
            }
            
            string? connectionString = Environment.GetEnvironmentVariable("database_connectionString");
            
            if (connectionString == null)
            {
                Console.WriteLine(connectionString);
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