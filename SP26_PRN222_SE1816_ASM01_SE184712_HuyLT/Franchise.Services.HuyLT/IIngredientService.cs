using Franchise.Entities.HuyLT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Services.HuyLT
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetAllActiveIngredientsAsync();
        Task<List<Ingredient>> GetAllIngredientsAsync();                                              // Dùng cho dropdown (không phân trang)
        Task<(List<Ingredient> Items, int TotalCount)> GetAllIngredientsPagedAsync(int pageIndex, int pageSize, string? search = null); // Dùng cho trang list
        Task<Ingredient?> GetIngredientsByIdAsync(int ingredientId);

        Task AddIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int ingredientId);
        Task UpdateIngredientAsync(Ingredient ingredient);

    }
}
