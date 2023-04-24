using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Models.Dto.Get;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface ISellerRepository : IRepository<Seller>
    {
        Task<Dictionary<string, object>> CreateSellerAsync(CreateNewSellerDTO entity);
        Task<List<NewSellerDTO>> GetNewSellers();
        public Task<dynamic> GetRecentNewSeller();
        public Task<List<dynamic>> GetAllSeller();
        public Task<List<dynamic>> GetSellerReport(int status);
        public Task<dynamic> GetSellerDetails(int id);


    }
}
