using Franchise.Entities.HuyLT.Models;
using Franchise.Repositories.HuyLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Services.HuyLT
{
    public class IngredientService : IIngredientService 
    {
        private readonly IngredientRepository _repo;
        public IngredientService(IngredientRepository repo)
        {
            _repo = repo;
        }
        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            await _repo.CreateAsync(ingredient);
        }
        public async Task DeleteIngredientAsync(int ingredientId)
        {
            var ingre = await _repo.GetIngredientsByIdAsync(ingredientId);
            if (ingre != null)
            {
                ingre.IsActive = true;
                await _repo.UpdateAsync(ingre);
            }
        }

        public async Task<List<Ingredient>> GetAllActiveIngredientsAsync()
        {
            return await _repo.GetAllActiveIngredientsAsync();
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _repo.GetAllIngredientsAsync();
        }

        public async Task<(List<Ingredient> Items, int TotalCount)> GetAllIngredientsPagedAsync(int pageIndex, int pageSize, string? search = null)
        {
            return await _repo.GetAllIngredientsPagedAsync(pageIndex, pageSize, search);
        }

        public async Task<Ingredient?> GetIngredientsByIdAsync(int ingredientId)
        {
            return await _repo.GetIngredientsByIdAsync(ingredientId);
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await _repo.UpdateAsync(ingredient);
        }

       
    }
}
