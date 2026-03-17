using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Ingredient
{
    public class CreateModel : PageModel
    {
        private readonly IngredientService _service;
        private readonly Franchise.Repositories.HuyLT.DBContext.FranchiseManagementContext _context;
        public CreateModel(IngredientService service, Franchise.Repositories.HuyLT.DBContext.FranchiseManagementContext context)
        {
            _service = service;
            _context = context;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.Ingredient Input { get; set; } = new Franchise.Entities.HuyLT.Models.Ingredient();
        public List<SelectListItem> SupplierList { get; set; } = new();

        public void OnGet()
        {
            LoadSuppliers();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSuppliers();
                return Page();
            }
            await _service.AddIngredientAsync(Input);
            return RedirectToPage("./Index");
        }

        private void LoadSuppliers()
        {
            SupplierList = _context.Suppliers
                .Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.SupplierName
                }).ToList();
        }
    }
}