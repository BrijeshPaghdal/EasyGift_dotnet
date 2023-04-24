using EasyGift_API.Models;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UpdateAsync(Product entity);
        Task<dynamic> GetPastWeekAddedProduct();
        Task<List<dynamic>> GetProducts(int id = 0);
        Task<dynamic> GetProductDetail(int id);
    }
}
