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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task CreateAsync(Product entity)
        {
            await _db.Product.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Product> GetAsync(Expression<Func<Product,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Product> query = _db.Product;
            if(!tracked)
                query= query.AsNoTracking();
            if (filter != null)
                query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllAsync(Expression<Func<Product,bool>> filter = null)
        {
            IQueryable<Product> query = _db.Product;
            if(filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }


        public async Task RemoveAsync(Product entity)
        {
            _db.Product.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
             _db.Product.Update(entity);
            await SaveAsync();
        }
    }
}
