using EasyGift_API.Models;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UpdateAsync(Product entity);
        Task<dynamic> GetPastAddedProduct(int shopId = 0, int limit = 7);
        Task<dynamic> GetProductDetail(int id);
        Task<List<dynamic>> GetFilteredProducts(Filter filter);
    }
}
