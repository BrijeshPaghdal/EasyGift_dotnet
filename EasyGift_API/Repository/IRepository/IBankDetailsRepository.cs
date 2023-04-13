using EasyGift_API.Models;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IBankDetailsRepository : IRepository<BankDetails>
    {
        Task<BankDetails> UpdateAsync(BankDetails entity);
    }
}
