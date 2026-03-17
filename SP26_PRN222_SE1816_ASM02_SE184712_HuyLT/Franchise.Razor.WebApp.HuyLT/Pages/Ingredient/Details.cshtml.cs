using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Franchise.Entities.HuyLT.Models;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Ingredient
{
    public class DetailsModel : PageModel
    {
        private readonly IngredientService _service;
        public DetailsModel(IngredientService service)
        {
            _service = service;
        }

        public global::Franchise.Entities.HuyLT.Models.Ingredient? Item { get; set; }

        public async Task OnGetAsync(int id)
        {
            Item = await _service.GetIngredientsByIdAsync(id);
        }
    }
}