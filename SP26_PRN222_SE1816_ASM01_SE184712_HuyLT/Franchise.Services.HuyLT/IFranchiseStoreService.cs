using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Services.HuyLT 
{ 

    public interface IFranchiseStoreService
    {
        Task<Franchise.Entities.HuyLT.Models.FranchiseStore?> GetStoreByLoginAsync(string email, string password);
        Task<List<Franchise.Entities.HuyLT.Models.FranchiseStore>> GetAllStoresAsync();
        Task<Franchise.Entities.HuyLT.Models.FranchiseStore?> GetStoreByIdAsync(int id);
        Task<Franchise.Entities.HuyLT.Models.FranchiseStore?> GetStoreByEmailAsync(string email);
        Task UpdateStoreAsync(Franchise.Entities.HuyLT.Models.FranchiseStore store);
    }
}
