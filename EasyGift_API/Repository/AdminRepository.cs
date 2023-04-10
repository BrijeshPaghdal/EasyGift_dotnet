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
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly ApplicationDbContext _db;
       public AdminRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
       
        public async Task<Admin> UpdateAsync(Admin entity)
        {
            _db.Admin.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
