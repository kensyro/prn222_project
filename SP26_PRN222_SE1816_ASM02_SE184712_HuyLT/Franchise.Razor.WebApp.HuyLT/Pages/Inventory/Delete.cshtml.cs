using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Inventory
{
    public class DeleteModel : PageModel
    {
        private readonly InventoryService _service;
        public DeleteModel(InventoryService service)
        {
            _service = service;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.Inventory? Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _service.GetInventoryByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Item != null)
            {
                await _service.DeleteInventoryAsync(Item.InventoryId);
            }
            return RedirectToPage("./Index");
        }
    }
}