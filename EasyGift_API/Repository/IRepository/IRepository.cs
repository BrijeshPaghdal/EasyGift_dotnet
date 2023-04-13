using EasyGift_API.Models;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int limit = 0);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task SaveAsync();
    }
}
