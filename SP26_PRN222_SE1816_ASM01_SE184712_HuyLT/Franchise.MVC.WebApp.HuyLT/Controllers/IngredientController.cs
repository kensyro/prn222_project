using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Franchise.MVC.WebApp.HuyLT.Filters;

namespace Franchise.MVC.WebApp.HuyLT.Controllers
{
    // Controller quản lý Ingredient, tương ứng với bảng Ingredient trong database
    // Tên controller giống tên bảng Ingredient theo yêu cầu
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class IngredientController : Controller
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            int pageSize = 5;
            int pageIndex = page - 1;

            var result = await _service.GetAllIngredientsPagedAsync(pageIndex, pageSize, search);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)result.TotalCount / pageSize);
            ViewData["Search"] = search;

            return View(result.Items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _service.GetIngredientsByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Franchise.Entities.HuyLT.Models.Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await _service.AddIngredientAsync(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _service.GetIngredientsByIdAsync(id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Franchise.Entities.HuyLT.Models.Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateIngredientAsync(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _service.GetIngredientsByIdAsync(id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteIngredientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
