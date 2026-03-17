using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Franchise.Razor.WebApp.HuyLT.Pages.FranchiseStore
{
    public class EditModel : PageModel
    {
        private readonly FranchiseStoreService _service;
        public EditModel(FranchiseStoreService service)
        {
            _service = service;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.FranchiseStore Input { get; set; } = new Franchise.Entities.HuyLT.Models.FranchiseStore();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _service.GetStoreByIdAsync(id);
            if (item == null) return RedirectToPage("./Index");
            Input = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _service.UpdateStoreAsync(Input);
            return RedirectToPage("./Index");
        }
    }
}