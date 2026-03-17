using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Inventory
{
    public class EditModel : PageModel
    {
        private readonly InventoryService _service;
        public EditModel(InventoryService service)
        {
            _service = service;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.Inventory Input { get; set; } = new Franchise.Entities.HuyLT.Models.Inventory();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _service.GetInventoryByIdAsync(id);
            if (item == null) return RedirectToPage("./Index");
            Input = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _service.UpdateInventoryAsync(Input);
            return RedirectToPage("./Index");
        }
    }
}