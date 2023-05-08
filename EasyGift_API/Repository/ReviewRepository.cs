using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasyGift_API.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<dynamic> GetProductReviews(int ShopId=0,int ProductId=0,int limit=0)
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetProductReviews", connection))
                {
                    cmd.Parameters.AddWithValue("@shop_id", ShopId);
                    cmd.Parameters.AddWithValue("@prod_id", ProductId);
                    cmd.Parameters.AddWithValue("@limit", limit);

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
                            dynamic data = new ExpandoObject();
                            var dict = data as IDictionary<string, object>;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string name = reader.GetName(i);
                                object value = reader.GetValue(i);

                                dict.Add(name, value);
                            }

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
