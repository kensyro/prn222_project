using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Franchise.Razor.WebApp.HuyLT.Pages.FranchiseStore
{
    public class DeleteModel : PageModel
    {
        private readonly FranchiseStoreService _service;
        public DeleteModel(FranchiseStoreService service)
        {
            _service = service;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.FranchiseStore? Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _service.GetStoreByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _service.DeleteStoreAsync(id);
            return RedirectToPage("./Index");
        }
    }
}