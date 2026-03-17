using Franchise.Entities.HuyLT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Services.HuyLT
{
    public interface IInventoryService
    {
        /*Thay đổi định nghĩa hàm GetAll*/
        Task<(List<Inventory> Items, int TotalCount)> GetAllInventoryPagedAsync(int pageIndex, int pageSize, string? search = null);
        Task<Inventory?> GetInventoryByIdAsync(int inventoryId);

        Task<List<Inventory>> GetInventoryByIngredientIdAsync(int ingredientId);

        Task AddInventoryAsync(Inventory inventory);
        Task UpdateInventoryAsync(Inventory inventory);
        Task DeleteInventoryAsync(int inventoryId);


       Task<bool> SkuExistsAsync(string sku);


    }
}
