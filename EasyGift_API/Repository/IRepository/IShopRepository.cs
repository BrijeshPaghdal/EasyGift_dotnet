using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IShopRepository : IRepository<Shop>
    {
        public Task<dynamic> GetTotalOrder(int id);

    }
}
