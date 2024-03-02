using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManagement.API.Attributes;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Domain.Entities.DTOs;
using RecipeManagement.Domain.Entities.Enums;
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
        [IdentityFilter(Permission.CreateRecipe)]
        public async Task<ActionResult<Recipe>> CreateRecipe(RecipeDTO model)
        {
            var result = await _recipeService.Create(model);

            return Ok(result);
        }

        [HttpGet]
        [IdentityFilter(Permission.GetAllRecipes)]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
        {
            var result = await _recipeService.GetAll();

            return Ok(result);
        }

        [HttpGet]
        [IdentityFilter(Permission.GetRecipeById)]
        public async Task<ActionResult<Recipe>> GetRecipeById(int id)
        {
            var result = await _recipeService.GetById(id);

            return Ok(result);
        }

        [HttpPut]
        [IdentityFilter(Permission.UpdateRecipe)]
        public async Task<ActionResult<Recipe>> UpdateRecipe(int id, RecipeDTO model)
        {
            var result = await _recipeService.Update(id, model);

            return Ok(result);
        }

        [HttpDelete]
        [IdentityFilter(Permission.DeleteRecipe)]
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
