using EasyGift_API.Models;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IBankDetailsRepository : IRepository<BankDetails>
    {
        Task<BankDetails> UpdateAsync(BankDetails entity);
        Task<dynamic> GetDistinctBankCountry();
        Task<dynamic> GetDistinctBankStates(string BankCountry);
        Task<dynamic> GetDistinctBankCities(string BankStates);
        Task<dynamic> GetDistinctBankNames(string BankCity);
        Task<dynamic> GetDistinctBankBranch(string BankState, string BankCity, string BankName);
        Task<dynamic> GetDistinctBankDetails(string BankName, string BankBranch);
    }
}
