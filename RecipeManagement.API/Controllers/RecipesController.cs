using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Domain.Entities.DTOs;
using RecipeManagement.Domain.Entities.Models;
using RecipeManagement.Domain.Entities.ViewModels;

namespace RecipeManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe(RecipeDTO model)
        {
            var result = await _recipeService.Create(model);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
        {
            var result = await _recipeService.GetAll();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Recipe>> GetRecipeById(int id)
        {
            var result = await _recipeService.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Recipe>> GetRecipeByTag(string tag)
        {
            var result = await _recipeService.GetByTag(tag);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Recipe>> UpdateRecipe(int id, RecipeDTO model)
        {
            var result = await _recipeService.Update(id, model);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteRecipe(int id)
        {
            var result = await _recipeService.Delete(x => x.Id == id);

            if (!result)
            {
                return BadRequest("Something went wrong...");
            }

            return Ok("Deleted successfully");
        }
    }
}
