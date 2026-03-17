using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Franchise.Razor.WebApp.HuyLT.Pages.Inventory
{
    public class CreateModel : PageModel
    {
        private readonly InventoryService _service;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(InventoryService service, ILogger<CreateModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.Inventory Input { get; set; } = new Franchise.Entities.HuyLT.Models.Inventory();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    var entry = kvp.Value;
                    foreach (var error in entry.Errors)
                    {
                        _logger.LogWarning("ModelState error for {Key}: {Error}", key, error.ErrorMessage);
                    }
                }
                return Page();
            }

            try
            {
                await _service.AddInventoryAsync(Input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Inventory");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the inventory: " + ex.Message);
                return Page();
            }

            _logger.LogInformation("Inventory created: {Id}", Input.InventoryId);
            return RedirectToPage("./Index");
        }
    }
}