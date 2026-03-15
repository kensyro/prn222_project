using Franchise.Entities.HuyLT.Models;
using Franchise.Repositories.HuyLT.Basic;
using Franchise.Repositories.HuyLT.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Repositories.HuyLT
{
    public class IngredientRepository : GenericRepository<Franchise.Entities.HuyLT.Models.Ingredient>
    {
        public IngredientRepository(FranchiseManagementContext context) 
        {
           _context = context;
        }
        public async Task<List<Franchise.Entities.HuyLT.Models.Ingredient>> GetAllActiveIngredientsAsync()
        {
            return await _context.Ingredients
                .Where(i => i.IsActive == true) // Dùng IsActive thay vì Status
                .OrderBy(i => i.IngredientName)  // Dùng IngredientName 
                .ToListAsync();
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients
                .OrderBy(i => i.IngredientName)
                .ToListAsync(); 
        }
        public async Task<(List<Ingredient> Items, int TotalCount)> GetAllIngredientsPagedAsync(
            int pageIndex, int pageSize, string? search = null)
        {
            var query = _context.Ingredients.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(i => i.IngredientName.Contains(search));
            }
            int total = await query.CountAsync();
            var items = await query
                .OrderBy(i => i.IngredientName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task<Ingredient>  GetIngredientsByIdAsync(int ingredientId)
        {
            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(i => i.IngredientId == ingredientId);
            return ingredient!;
        }

    }
}
