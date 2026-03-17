using Franchise.Entities.HuyLT.Models;
using Franchise.Services.HuyLT;
using Microsoft.AspNetCore.SignalR;

namespace Franchise.Razor.WebApp.HuyLT.Hubs
{
    public class FranchiseHub : Hub
    {
        private readonly IngredientService _ingredientService;
        public FranchiseHub(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task HubCreateIngredient(Ingredient ingredient)
        {
            await _ingredientService.AddIngredientAsync(ingredient);
            await Clients.All.SendAsync("ReceiveIngredientCreated", ingredient);
        }

        public async Task HubUpdateIngredient(Ingredient ingredient)
        {
            await _ingredientService.UpdateIngredientAsync(ingredient);
            await Clients.All.SendAsync("ReceiveIngredientUpdated", ingredient);
        }

        public async Task HubDeleteIngredient(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);
            await Clients.All.SendAsync("ReceiveIngredientDeleted", id);
        }
    }
}
