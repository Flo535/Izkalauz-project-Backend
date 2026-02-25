using IzKalauzBackend.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<WeeklyMenuItem> WeeklyMenuItems { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<ShoppingList> ShoppingLists { get; set; } = null!;
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; } = null!;
        public DbSet<ShoppingListRecipe> ShoppingListRecipes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== User tábla =====
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();

            // ===== Recipe tábla =====
            modelBuilder.Entity<Recipe>().HasKey(r => r.Id);
            modelBuilder.Entity<Recipe>().Property(r => r.Title).IsRequired();

            // JAVÍTÁS: Nem kötelező mezők (eltávolítva az IsRequired), hogy ne legyen mentési hiba
            modelBuilder.Entity<Recipe>().Property(r => r.Description).IsRequired(false);
            modelBuilder.Entity<Recipe>().Property(r => r.HowToText).IsRequired(false);
            modelBuilder.Entity<Recipe>().Property(r => r.ImagePath).IsRequired(false);

            modelBuilder.Entity<Recipe>().Property(r => r.AuthorEmail).IsRequired();
            modelBuilder.Entity<Recipe>().Property(r => r.Category).IsRequired();

            modelBuilder.Entity<Recipe>().Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Recipe>().Property(r => r.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            // ===== Ingredients (Kényszerített törléssel) =====
            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade); // Ha törlünk/módosítunk, a régi hozzávalók ne okozzanak hibát

            // ===== ShoppingList =====
            modelBuilder.Entity<ShoppingList>().HasKey(s => s.Id);
            modelBuilder.Entity<ShoppingList>().Property(s => s.AuthorEmail).IsRequired();
            modelBuilder.Entity<ShoppingList>().HasMany(s => s.Items).WithOne(i => i.ShoppingList).HasForeignKey(i => i.ShoppingListId).OnDelete(DeleteBehavior.Cascade);

            // ===== ShoppingListRecipe =====
            modelBuilder.Entity<ShoppingListRecipe>().HasOne(sr => sr.ShoppingList).WithMany(s => s.Recipes).HasForeignKey(sr => sr.ShoppingListId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ShoppingListRecipe>().HasOne(sr => sr.Recipe).WithMany().HasForeignKey(sr => sr.RecipeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}