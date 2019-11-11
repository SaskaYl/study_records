using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Studyrecord.Models
{
    public partial class OpintopäiväkirjaContext : DbContext
    {
        public OpintopäiväkirjaContext()
        {
        }

        public OpintopäiväkirjaContext(DbContextOptions<OpintopäiväkirjaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Topic> Topic { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=Opintopäiväkirja;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.Property(e => e.CompletionDate)
                    .HasColumnName("completionDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Estimatedtime)
                    .HasColumnName("estimatedtime")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Source)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.Property(e => e.TimeSpent).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
