using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
        public async Task<dynamic> GetPastWeekAddedProduct()
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("RecentlyAddedProductforChart", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    //cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (await reader.ReadAsync())
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data["total"] = (int)reader["total"];
                            data["add_date"] = (DateTime)reader["add_date"];
                            datas.Add(data);
                        }
                    }
                }
                connection.Close();
            }
            return datas;
        }
    }
}
