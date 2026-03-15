using Franchise.Entities.HuyLT.Models;
using Franchise.Repositories.HuyLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Franchise.Services.HuyLT
{
    public class FranchiseStoreService : IFranchiseStoreService
    {
        private readonly FranchiseStoreRepository _franchiseStoreRepository;

        public FranchiseStoreService(FranchiseStoreRepository franchiseStoreRepository)
        {
            _franchiseStoreRepository = franchiseStoreRepository;
        }

        public async Task<FranchiseStore?> GetStoreByLoginAsync(string email, string password)
        {
            return await _franchiseStoreRepository.GetStoreByLoginAsync(email, password);
        }

        public async Task<FranchiseStore?> GetStoreByEmailAsync(string email)
        {
            return await _franchiseStoreRepository.GetStoreByEmailAsync(email);
        }

        public async Task<List<FranchiseStore>> GetAllStoresAsync()
        {
            // Sử dụng hàm GetAllAsync từ GenericRepository
            return await _franchiseStoreRepository.GetAllAsync();
        }

        public async Task<FranchiseStore?> GetStoreByIdAsync(int id)
        {
            // Sử dụng hàm GetByIdAsync từ GenericRepository
            return await _franchiseStoreRepository.GetByIdAsync(id);
        }

        public async Task UpdateStoreAsync(FranchiseStore store)
        {
            try
            {
                await _franchiseStoreRepository.UpdateAsync(store);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật cửa hàng: " + ex.Message);
            }
        }

    }
}
