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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _db;
       public AddressRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
       
        public async Task<Address> UpdateAsync(Address entity)
        {
            _db.Address.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
