using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Franchise.Razor.WebApp.HuyLT.Pages.FranchiseStore
{
    public class DetailsModel : PageModel
    {
        private readonly FranchiseStoreService _service;
        public DetailsModel(FranchiseStoreService service)
        {
            _service = service;
        }

        public Franchise.Entities.HuyLT.Models.FranchiseStore? Item { get; set; }

        public async Task OnGetAsync(int id)
        {
            Item = await _service.GetStoreByIdAsync(id);
        }
    }
}