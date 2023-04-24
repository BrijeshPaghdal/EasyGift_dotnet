using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<dynamic> GetRecentNewCustomer();
        public Task<List<dynamic>> GetAllCustomers();
        public Task<List<dynamic>> GetUserReport(int status);

    }
}
