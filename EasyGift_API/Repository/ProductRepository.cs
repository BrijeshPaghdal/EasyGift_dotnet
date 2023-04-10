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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
       public ProductRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
       
        public async Task<Product> UpdateAsync(Product entity)
        {
            entity.UpdateDate= DateTime.Now;
            _db.Product.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
