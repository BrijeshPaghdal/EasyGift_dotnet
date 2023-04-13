using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EasyGift_API.Repository
{
    public class BankDetailsRepository : Repository<BankDetails>, IBankDetailsRepository
    {
        private readonly ApplicationDbContext _db;
       public BankDetailsRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
       
        public async Task<BankDetails> UpdateAsync(BankDetails entity)
        {
            _db.BankDetails.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
