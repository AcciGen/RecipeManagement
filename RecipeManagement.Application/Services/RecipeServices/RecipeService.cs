using RecipeManagement.Application.Abstractions;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Domain.Entities.DTOs;
using RecipeManagement.Domain.Entities.Models;
using System.Linq.Expressions;

namespace RecipeManagement.Application.Services.RecipeServices
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<Recipe> Create(RecipeDTO recipeDTO)
        {
            var recipe = new Recipe()
            {
                Title = recipeDTO.Title,
                Ingredients = recipeDTO.Ingredients,
                Instructions = recipeDTO.Instructions,
                DifficultyLevel = recipeDTO.DifficultyLevel,
                Author = recipeDTO.Author,
                Rating = recipeDTO.Rating
            };

            var result = await _recipeRepository.Create(recipe);
            return result;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            var result = await _recipeRepository.GetAll();
            return result;
        }

        public async Task<Recipe> GetById(int id)
        {
            var result = await _recipeRepository.GetByAny(x => x.Id == id);
            return result;
        }

        public async Task<Recipe> Update(int id, RecipeDTO recipeDTO)
        {
            var res = await _recipeRepository.GetByAny(x => x.Id == id);

            if (res != null)
            {
                var recipe = new Recipe()
                {
                    Title = recipeDTO.Title,
                    Ingredients = recipeDTO.Ingredients,
                    Instructions = recipeDTO.Instructions,
                    DifficultyLevel = recipeDTO.DifficultyLevel,
                    Author = recipeDTO.Author,
                    Rating = recipeDTO.Rating
                };
                var result = await _recipeRepository.Update(recipe);

                return result;
            }
            return null!;
        }

        public async Task<bool> Delete(Expression<Func<Recipe, bool>> expression)
        {
            var result = await _recipeRepository.Delete(expression);

            return result;
        }
    }
}
