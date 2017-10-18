using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace XinematriX.DataAccess.DBModels
{
    public partial class XinematriXContext : DbContext
    {
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieDatePromos> MovieDatePromos { get; set; }
        public virtual DbSet<MovieDayPromos> MovieDayPromos { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<MoviePolls> MoviePolls { get; set; }
        public virtual DbSet<MoviePollsOptions> MoviePollsOptions { get; set; }
        public virtual DbSet<MovieRating> MovieRating { get; set; }
        public virtual DbSet<WebAdminAuth> WebAdminAuth { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
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

            modelBuilder.Entity<MovieDatePromos>(entity =>
            {
                entity.Property(e => e.PromoDate)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PromoDescription)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovieDayPromos>(entity =>
            {
                entity.Property(e => e.PromoDay)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PromoDescription)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.Property(e => e.MovieGenreType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MoviePolls>(entity =>
            {
                entity.Property(e => e.Active)
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("('Y')");

                entity.Property(e => e.PollQuestion)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MoviePollsOptions>(entity =>
            {
                entity.Property(e => e.Options)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.MoviePolls)
                    .WithMany(p => p.MoviePollsOptions)
                    .HasForeignKey(d => d.MoviePollsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MoviePolls_MoviePollsOptions");
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
