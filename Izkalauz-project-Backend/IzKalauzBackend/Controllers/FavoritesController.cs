using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IzKalauzBackend.Data;
using IzKalauzBackend.Models;
using IzKalauzBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/Favorites                  A bejelentkezett felhasználó kedvencei
        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var favorites = await _context.FavoriteRecipes
                .Where(f => f.UserId == userId)
                .Include(f => f.Recipe)
                .OrderByDescending(f => f.AddedAt)
                .Select(f => new FavoriteResponseDto
                {
                    Id = f.Id,
                    RecipeId = f.RecipeId,
                    Title = f.Recipe.Title,
                    Category = f.Recipe.Category,
                    Description = f.Recipe.Description,
                    ImagePath = f.Recipe.ImagePath,
                    AddedAt = f.AddedAt
                })
                .ToListAsync();

            return Ok(favorites);
        }

        // GET: /api/Favorites/ids                  Csak a kedvelt receptek listája
        [HttpGet("ids")]
        public async Task<IActionResult> GetFavoriteIds()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var ids = await _context.FavoriteRecipes
                .Where(f => f.UserId == userId)
                .Select(f => f.RecipeId)
                .ToListAsync();

            return Ok(ids);
        }

        // POST: /api/Favorites/{recipeId}          Kedvencekhez adás
        [HttpPost("{recipeId:guid}")]
        public async Task<IActionResult> AddFavorite(Guid recipeId)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null)
                return NotFound(new { message = "Recept nem található." });

            var alreadyExists = await _context.FavoriteRecipes
                .AnyAsync(f => f.UserId == userId && f.RecipeId == recipeId);

            if (alreadyExists)
                return Conflict(new { message = "Ez a recept már a kedvenceid között van." });

            var favorite = new FavoriteRecipe
            {
                UserId = userId.Value,
                RecipeId = recipeId
            };

            _context.FavoriteRecipes.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Recept hozzáadva a kedvencekhez.", favoriteId = favorite.Id });
        }

        // DELETE: /api/Favorites/{recipeId}        Kedvencekből eltávolítás
        [HttpDelete("{recipeId:guid}")]
        public async Task<IActionResult> RemoveFavorite(Guid recipeId)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var favorite = await _context.FavoriteRecipes
                .FirstOrDefaultAsync(f => f.UserId == userId && f.RecipeId == recipeId);

            if (favorite == null)
                return NotFound(new { message = "Ez a recept nincs a kedvenceid között." });

            _context.FavoriteRecipes.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Recept eltávolítva a kedvencekből." });
        }

        // Segédmetódus - UserId kiolvasása tokenből
        private Guid? GetUserId()
        {
            var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(nameIdentifier, out var userId))
                return userId;
            return null;
        }
    }
}