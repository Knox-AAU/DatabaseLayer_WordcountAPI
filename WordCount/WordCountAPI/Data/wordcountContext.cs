using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WordCount
{
    public partial class wordcountContext : DbContext
    {
        public wordcountContext()
        {
        }

        public wordcountContext(DbContextOptions<wordcountContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<JsonSchema> JsonSchemas { get; set; }
        public virtual DbSet<OccursIn> OccursIns { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<WordRatio> WordRatios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseNpgsql("UserID=postgres;Password=Sysadmins.;Host=localhost;Port=5002;Database=wordcount;");
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("database_connectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("citext")
                .HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.PublisherNameNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.PublisherName)
                    .HasConstraintName("Article_PublisherName_fkey");
            });

            modelBuilder.Entity<JsonSchema>(entity =>
            {
                entity.HasKey(e => e.SchemaName);

                entity.ToTable("JsonSchema");

                entity.Property(e => e.JsonString).HasColumnType("jsonb");
            });

            modelBuilder.Entity<OccursIn>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.Word })
                    .HasName("OccursIn_pkey");

                entity.ToTable("OccursIn");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.OccursIns)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OccursIn_ArticleId_fkey");

                entity.HasOne(d => d.WordNavigation)
                    .WithMany(p => p.OccursIns)
                    .HasForeignKey(d => d.Word)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OccursIn_Word_fkey");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PublisherName);

                entity.ToTable("Publisher");

                entity.HasIndex(e => e.PublisherName, "IX_Publisher_PublisherName")
                    .IsUnique();
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.HasKey(e => e.Text)
                    .HasName("Literal_PRMK");

                entity.ToTable("Word");
            });

            modelBuilder.Entity<WordRatio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WordRatio");
            });

            modelBuilder.HasSequence("Article_Id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
