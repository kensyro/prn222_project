using Franchise.Entities.HuyLT.Models;
using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Franchise.MVC.WebApp.HuyLT.Filters;

namespace Franchise.MVC.WebApp.HuyLT.Controllers
{
    // Controller quản lý Inventory, tương ứng với bảng Inventory trong database
    // Kết hợp cả chức năng hiển thị (Public) và quản lý để đảm bảo "giống tên bảng"
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IIngredientService _ingredientService;

        public InventoryController(IInventoryService inventoryService, IIngredientService ingredientService)
        {
            _inventoryService = inventoryService;
            _ingredientService = ingredientService;
        }

        public async Task<IActionResult> Index(string? search, int? ingredientId, int page = 1)
        {
            int pageSize = 8;
            int pageIndex = page - 1;

            var allIngredients = await _ingredientService.GetAllIngredientsAsync();
            ViewData["Ingredients"] = allIngredients;

            List<Inventory> inventories;
            int totalCount;

            // 2. Logic lọc dữ liệu
            if (ingredientId.HasValue)
            {
                // Lọc theo loại nguyên liệu
                inventories = await _inventoryService.GetInventoryByIngredientIdAsync(ingredientId.Value);
                totalCount = inventories.Count;

                // Thực hiện phân trang thủ công trên list (InMemory Paging)
                inventories = inventories.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                var currentIng = allIngredients.FirstOrDefault(i => i.IngredientId == ingredientId);
                ViewData["PageTitle"] = currentIng?.IngredientName ?? "Nguyên liệu";
            }
            else
            {
                // Lọc theo search hoặc lấy tất cả (Server-side Paging)
                var result = await _inventoryService.GetAllInventoryPagedAsync(pageIndex, pageSize, search);
                inventories = result.Items;
                totalCount = result.TotalCount;

                ViewData["PageTitle"] = string.IsNullOrEmpty(search) ? "Tất cả kho hàng" : $"Kết quả tìm kiếm: {search}";
            }

            // 3. Đưa dữ liệu ra ViewData cho Pagination ở View
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewData["Search"] = search;
            ViewData["IngredientId"] = ingredientId;

            return View(inventories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateIngredientsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _inventoryService.AddInventoryAsync(inventory);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo kho hàng: {ex.Message}");
                }
            }
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            ViewData["IngredientId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ingredients, "IngredientId", "IngredientName", inventory.IngredientId); return View(inventory);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null) return NotFound();

            await PopulateIngredientsDropDownList(inventory.IngredientId);
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Inventory inventory)
        {
            if (id != inventory.InventoryId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _inventoryService.UpdateInventoryAsync(inventory);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật kho hàng: {ex.Message}");
                }
            }
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            ViewData["IngredientId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ingredients, "IngredientId", "IngredientName", inventory.IngredientId);
            return View(inventory);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null) return NotFound();
            return View(inventory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _inventoryService.DeleteInventoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateIngredientsDropDownList(object? selectedIngredient = null)
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            ViewData["IngredientId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ingredients, "IngredientId", "IngredientName", selectedIngredient);
        }
    }
}
