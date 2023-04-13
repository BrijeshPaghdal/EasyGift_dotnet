using EasyGift_API.Models;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> UpdateAsync(Address entity);
    }
}
