using Microsoft.EntityFrameworkCore;

namespace URLShortener.Models
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>()
                .HasIndex(u => u.ShortenedCode)
                .IsUnique();

            modelBuilder.Entity<Url>()
                .Property(u => u.HitCount)
                .HasDefaultValue(0);
        }
    }
}