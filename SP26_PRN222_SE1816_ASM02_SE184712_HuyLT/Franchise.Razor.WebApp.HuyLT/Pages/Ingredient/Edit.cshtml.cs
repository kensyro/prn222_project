using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Ingredient
{
    public class EditModel : PageModel
    {
        private readonly IngredientService _service;
        private readonly Franchise.Repositories.HuyLT.DBContext.FranchiseManagementContext _context;
        public EditModel(IngredientService service, Franchise.Repositories.HuyLT.DBContext.FranchiseManagementContext context)
        {
            _service = service;
            _context = context;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.Ingredient Input { get; set; } = new Franchise.Entities.HuyLT.Models.Ingredient();
        public List<SelectListItem> SupplierList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _service.GetIngredientsByIdAsync(id);
            if (item == null) return RedirectToPage("./Index");
            Input = item;
            SupplierList = _context.Suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierId.ToString(),
                Text = s.SupplierName
            }).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SupplierList = _context.Suppliers.Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.SupplierName
                }).ToList();
                return Page();
            }
            await _service.UpdateIngredientAsync(Input);
            return RedirectToPage("./Index");
        }
    }
}