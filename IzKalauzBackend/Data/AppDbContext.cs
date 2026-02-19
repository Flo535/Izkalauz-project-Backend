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
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<WeeklyMenuItem> WeeklyMenuItems { get; set; }

        //Jegyzet
        public DbSet<Note> Notes { get; set; } = null!;

        //Bevásárló lista
        public DbSet<ShoppingList> ShoppingLists { get; set; } = null!;
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; } = null!;
        public DbSet<ShoppingListRecipe> ShoppingListRecipes { get; set; } = null!;

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
            
            modelBuilder.Entity<Recipe>()
                .Property(r => r.AuthorEmail)
                .IsRequired();

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Category)
                .IsRequired();

            modelBuilder.Entity<Recipe>()
               .Property(r => r.HowToText)
               .IsRequired();

            modelBuilder.Entity<Recipe>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            //Ingredients
            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId);

            // ===== ShoppingList =====
            modelBuilder.Entity<ShoppingList>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ShoppingList>()
                .Property(s => s.Title)
                .IsRequired();

            modelBuilder.Entity<ShoppingList>()
                .Property(s => s.AuthorEmail)
                .IsRequired();

            modelBuilder.Entity<ShoppingList>()
                .HasMany(s => s.Items)
                .WithOne(i => i.ShoppingList)
                .HasForeignKey(i => i.ShoppingListId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== ShoppingListItem =====
            modelBuilder.Entity<ShoppingListItem>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<ShoppingListItem>()
                .Property(i => i.Name)
                .IsRequired();

            modelBuilder.Entity<ShoppingListItem>()
                .Property(i => i.Unit)
                .IsRequired();

            modelBuilder.Entity<ShoppingListItem>()
                .Property(i => i.Quantity)
                .IsRequired();

            // ===== ShoppingListRecipe =====
            modelBuilder.Entity<ShoppingListRecipe>()
                .HasOne(sr => sr.ShoppingList)
                .WithMany(s => s.Recipes)
                .HasForeignKey(sr => sr.ShoppingListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingListRecipe>()
                .HasOne(sr => sr.Recipe)
                .WithMany()
                .HasForeignKey(sr => sr.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
