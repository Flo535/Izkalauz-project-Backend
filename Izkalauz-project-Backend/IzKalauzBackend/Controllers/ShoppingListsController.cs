using System.Security.Claims;
using IzKalauzBackend.Data;
using IzKalauzBackend.DTOs;
using IzKalauzBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzKalauzBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShoppingListsController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserEmail()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Érvénytelen token.");
            return email;
        }


        [HttpGet]
        public async Task<ActionResult<List<ShoppingListDto>>> GetAll()
        {
            var email = GetUserEmail();

            var lists = await _context.ShoppingLists
                .Where(s => s.AuthorEmail == email)
                .Include(s => s.Items)
                .ToListAsync();

            return lists.Select(l => new ShoppingListDto
            {
                Id = l.Id,
                Title = l.Title,
                Items = l.Items.Select(i => new ShoppingListItemDto
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList()
            }).ToList();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateShoppingListDto dto)
        {
            var list = new ShoppingList
            {
                Title = dto.Title,
                AuthorEmail = GetUserEmail()
            };

            _context.ShoppingLists.Add(list);
            await _context.SaveChangesAsync();

            return Ok(list.Id);
        }


        [HttpPost("{shoppingListId}/add-recipe/{recipeId}")]
        public async Task<IActionResult> AddRecipe(Guid shoppingListId, Guid recipeId)
        {
            var email = GetUserEmail();

            var shoppingList = await _context.ShoppingLists
                .FirstOrDefaultAsync(s => s.Id == shoppingListId && s.AuthorEmail == email);

            if (shoppingList == null)
                return NotFound("Bevásárlólista nem található.");

            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
                return NotFound("Recept nem található.");

            // Tároljuk a receptet
            _context.ShoppingListRecipes.Add(new ShoppingListRecipe
            {
                ShoppingListId = shoppingListId,
                RecipeId = recipeId
            });

            // Közvetlenül a DB-ből kérjük le az itemeket, ne navigációs property-n át
            var existingItems = await _context.ShoppingListItems
                .Where(i => i.ShoppingListId == shoppingListId)
                .ToListAsync();

            foreach (var ingredient in recipe.Ingredients)
            {
                var existingItem = existingItems.FirstOrDefault(i =>
                    i.Name.ToLower() == ingredient.Name.ToLower() &&
                    i.Unit == ingredient.Unit);

                if (existingItem != null)
                {
                    existingItem.Quantity += ingredient.Quantity;
                    _context.ShoppingListItems.Update(existingItem);
                }
                else
                {
                    _context.ShoppingListItems.Add(new ShoppingListItem
                    {
                        Name = ingredient.Name,
                        Quantity = ingredient.Quantity,
                        Unit = ingredient.Unit,
                        ShoppingListId = shoppingListId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListDto>> Get(Guid id)
        {
            var email = GetUserEmail();

            var list = await _context.ShoppingLists
                .Include(s => s.Items)
                .Include(s => s.Recipes)
                    .ThenInclude(sr => sr.Recipe)
                .FirstOrDefaultAsync(s => s.Id == id && s.AuthorEmail == email);

            if (list == null)
                return NotFound();

            return new ShoppingListDto
            {
                Id = list.Id,
                Title = list.Title,
                Items = list.Items.Select(i => new ShoppingListItemDto
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList(),
                Recipes = list.Recipes.Select(sr => new RecipeDto
                {
                    Id = sr.Recipe.Id,
                    Title = sr.Recipe.Title,
                    Category = sr.Recipe.Category
                }).ToList()
            };
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var email = GetUserEmail();

            var list = await _context.ShoppingLists
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id && s.AuthorEmail == email);

            if (list == null)
                return NotFound("Bevásárlólista nem található.");

            _context.ShoppingLists.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{shoppingListId}/remove-recipe/{recipeId}")]
        public async Task<IActionResult> RemoveRecipe(Guid shoppingListId, Guid recipeId)
        {
            var email = GetUserEmail();

            var shoppingList = await _context.ShoppingLists
                .FirstOrDefaultAsync(s => s.Id == shoppingListId && s.AuthorEmail == email);

            if (shoppingList == null)
                return NotFound("Bevásárlólista nem található.");

            // ShoppingListRecipes kapcsolat törlése
            var listRecipe = await _context.ShoppingListRecipes
                .FirstOrDefaultAsync(sr => sr.ShoppingListId == shoppingListId && sr.RecipeId == recipeId);

            if (listRecipe != null)
            {
                _context.ShoppingListRecipes.Remove(listRecipe);
            }


            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
                return NotFound("Recept nem található.");

            // A listában lévő összes item lekérése
            var itemsToModify = await _context.ShoppingListItems
                .Where(i => i.ShoppingListId == shoppingListId)
                .ToListAsync();

            // Minden hozzávaló mennyiségének csökkentése vagy törlése
            foreach (var ingredient in recipe.Ingredients)
            {
                var existingItem = itemsToModify.FirstOrDefault(i =>
                    i.Name.ToLower() == ingredient.Name.ToLower() &&
                    i.Unit == ingredient.Unit);

                if (existingItem != null)
                {
                    existingItem.Quantity -= ingredient.Quantity;

                    if (existingItem.Quantity <= 0)
                    {
                        _context.ShoppingListItems.Remove(existingItem);
                    }
                    else
                    {
                        _context.ShoppingListItems.Update(existingItem);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
