using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly InventoryService _service;
        public IndexModel(InventoryService service)
        {
            _service = service;
        }

        public List<Franchise.Entities.HuyLT.Models.Inventory> Items { get; set; } = new List<Franchise.Entities.HuyLT.Models.Inventory>();
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public string? Search { get; set; }

        public async Task OnGetAsync(int? pageIndex, string? search)
        {
            PageIndex = pageIndex ?? 0;
            Search = search;
            var (items, total) = await _service.GetAllInventoryPagedAsync(PageIndex, PageSize, Search);
            Items = items;
            TotalCount = total;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                await _service.DeleteInventoryAsync(id);
                TempData["Message"] = "Inventory deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Delete failed: " + ex.Message;
            }
            return RedirectToPage();
        }
    }
}