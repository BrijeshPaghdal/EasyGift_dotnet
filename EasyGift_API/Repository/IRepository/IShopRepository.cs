using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IShopRepository : IRepository<Shop>
    {
        public Task<dynamic> GetTotalOrder(int id);
        Task<dynamic> GetTotalIncome(int shopId);
        Task<dynamic> GetTotalIncomeforChart(int shopId, int limit = 7);
        Task<dynamic> GetTotalProductSold(int id);
        Task<dynamic> GetTotalProductSoldforChart(int shopId, int limit = 7);
        Task<dynamic> CompleteOrder(int shopId, int OrderId);
        Task<List<dynamic>> GetProducts(int id = 0,int Status=3,int limit=0);
        Task<dynamic> GetOrders(int id,int status =3,int limit=0);
    }
}
