using Franchise.Entities.HuyLT.Models;
using Franchise.Repositories.HuyLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Services.HuyLT
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryRepository _repo;

        public InventoryService(InventoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<(List<Inventory> Items, int TotalCount)> GetAllInventoryPagedAsync(int pageIndex, int pageSize, string? search = null)
        {
            // Đảm bảo trong InventoryRepository hàm này tên là GetAllProductAsync hoặc đổi tên cho khớp
            return await _repo.GetAllInventoryPagedAsync(pageIndex, pageSize, search);
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
        {
            return await _repo.GetInventoryByIdAsync(inventoryId);
        }

        public async Task<List<Inventory>> GetInventoryByIngredientIdAsync(int ingredientId)
        {
            return await _repo.GetInventoryByIngredientIdAsync(ingredientId);
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            // Các hàm AddAsync, UpdateAsync thường nằm trong GenericRepository
            await _repo.CreateAsync(inventory);
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            await _repo.UpdateAsync(inventory);
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _repo.GetInventoryByIdAsync(inventoryId);
            if (inventory != null)
            {
                inventory.IsBlocked = true;
                await _repo.UpdateAsync(inventory);
            }
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _repo.SkuExistsAsync(sku);
        }
    }
}
