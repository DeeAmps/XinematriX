using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace XinematriX_DataAccess.DBModels
{
    public partial class XinematriXContext : DbContext
    {
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<MovieRating> MovieRating { get; set; }
        public virtual DbSet<WebAdminAuth> WebAdminAuth { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=xinematrix.database.windows.net;Database=XinematriX;User ID=Dee;Password=H3ll0w0rld123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Active)
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("('Y')");

                entity.Property(e => e.Casts)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ComingSoon)
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("('Y')");

                entity.Property(e => e.Genres)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PosterPath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Premiering)
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.Showing)
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.Synopsis)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TrailerPath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.MovieRating)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.MovieRatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_movie_rating");
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.Property(e => e.MovieGenreType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovieRating>(entity =>
            {
                entity.Property(e => e.MovieRatingType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WebAdminAuth>(entity =>
            {
                entity.Property(e => e.WebAdminPassword)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.WebAdminToken)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.WebAdminTokenExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.WebAdminTokenGeneratedDate).HasColumnType("datetime");

                entity.Property(e => e.WebAdminUsername)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
