using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Franchise.Razor.WebApp.HuyLT.Pages.FranchiseStore
{
    public class CreateModel : PageModel
    {
        private readonly FranchiseStoreService _service;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(FranchiseStoreService service, ILogger<CreateModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        [BindProperty]
        public Franchise.Entities.HuyLT.Models.FranchiseStore Input { get; set; } = new Franchise.Entities.HuyLT.Models.FranchiseStore();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Log model state errors for debugging
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
                await _service.AddStoreAsync(Input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating FranchiseStore");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the store.");
                return Page();
            }

            _logger.LogInformation("FranchiseStore created: {StoreName}", Input.StoreName);
            return RedirectToPage("./Index");
        }
    }
}