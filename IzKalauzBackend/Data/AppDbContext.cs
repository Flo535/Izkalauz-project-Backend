using IzKalauzBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IzKalauzBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<WeeklyMenuItem> WeeklyMenuItems { get; set; }
        public DbSet<Note> Notes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // ===== User tábla =====
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired();

            // ===== Recipe tábla =====
            modelBuilder.Entity<Recipe>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Title)
                .IsRequired();

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Description)
                .IsRequired();
            
            // Ingredients mezőt JSON-ként tároljuk
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Ingredients)
                .HasConversion(
                    v => JsonSerializer.Serialize(v ?? new List<string>(), (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v ?? "[]", (JsonSerializerOptions?)null) ?? new List<string>()
                );
            
            modelBuilder.Entity<Recipe>()
                .Property(r => r.AuthorEmail)
                .IsRequired();

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Category)
                .IsRequired();

            modelBuilder.Entity<Recipe>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
