using Franchise.Entities.HuyLT.Models;
using Franchise.Repositories.HuyLT.Basic;
using Franchise.Repositories.HuyLT.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Repositories.HuyLT
{
    public class FranchiseStoreRepository : GenericRepository<Franchise.Entities.HuyLT.Models.FranchiseStore>
    {
        public FranchiseStoreRepository() { }
        public FranchiseStoreRepository(FranchiseManagementContext context) 
        {
            _context = context;
        }
        public async Task<FranchiseStore?> GetStoreByEmailAsync(string email)
        {
            var user = await _context.FranchiseStores
                .Include(s => s.Inventories)
                .FirstOrDefaultAsync(s => s.Email == email);
            return user;
        }

        public async Task<FranchiseStore?> GetStoreByLoginAsync(string email, string password)
        {
            return await _context.FranchiseStores
                .FirstOrDefaultAsync(s => s.Email == email && s.PhoneNumber == password);
        }

    }
}
