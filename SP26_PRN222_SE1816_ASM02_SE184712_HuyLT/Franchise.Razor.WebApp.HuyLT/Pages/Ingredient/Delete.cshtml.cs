using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Franchise.Entities.HuyLT.Models;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Ingredient
{
    public class DeleteModel : PageModel
    {
        private readonly IngredientService _service;
        public DeleteModel(IngredientService service)
        {
            _service = service;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.Ingredient? Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _service.GetIngredientsByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Item != null)
            {
                await _service.DeleteIngredientAsync(Item.IngredientId);
            }
            return RedirectToPage("./Index");
        }
    }
}