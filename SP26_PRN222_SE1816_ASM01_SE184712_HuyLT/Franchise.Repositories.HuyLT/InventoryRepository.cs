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
    public class InventoryRepository : GenericRepository<Franchise.Entities.HuyLT.Models.Inventory>
    {
        public InventoryRepository(FranchiseManagementContext context) 
        {
            _context = context;
        }

        public async Task<(List<Inventory> Items, int TotalCount)> GetAllInventoryPagedAsync(int pageIndex, int pageSize, string search = null)
        {
            var query = _context.Inventories
                .Include(p => p.Ingredient)
                .Include(p => p.Type)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Ingredient.IngredientName.Contains(search));
            }

            int total = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.LastUpdated)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
        {
            var product = _context.Inventories
                .Include(p => p.Ingredient)
                .FirstOrDefaultAsync(p => p.InventoryId == inventoryId);
            return await product;
        }

        public async Task<List<Inventory>> GetInventoryByIngredientIdAsync(int ingredientId)
        {
            var products = _context.Inventories
                .Include(p => p.Ingredient)
                .Where(p => p.IngredientId == ingredientId)
                .OrderByDescending(p => p.LastUpdated)
                .ToListAsync();
            return await products;
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku)) return false;
            return await _context.Inventories.AnyAsync(p => p.Ingredient.Sku == sku);
        }
    }
}
