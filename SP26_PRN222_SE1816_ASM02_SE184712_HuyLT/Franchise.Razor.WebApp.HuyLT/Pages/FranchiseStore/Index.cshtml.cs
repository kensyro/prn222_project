using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Franchise.Razor.WebApp.HuyLT.Pages.FranchiseStore
{
    public class IndexModel : PageModel
    {
        private readonly FranchiseStoreService _service;
        public IndexModel(FranchiseStoreService service)
        {
            _service = service;
        }

        public List<Franchise.Entities.HuyLT.Models.FranchiseStore> Items { get; set; } = new List<Franchise.Entities.HuyLT.Models.FranchiseStore>();

        public async Task OnGetAsync()
        {
            Items = await _service.GetAllStoresAsync();
        }
    }
}