using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Inventory
{
    public class DetailsModel : PageModel
    {
        private readonly InventoryService _service;
        public DetailsModel(InventoryService service)
        {
            _service = service;
        }

        public Franchise.Entities.HuyLT.Models.Inventory? Item { get; set; }

        public async Task OnGetAsync(int id)
        {
            Item = await _service.GetInventoryByIdAsync(id);
        }
    }
}