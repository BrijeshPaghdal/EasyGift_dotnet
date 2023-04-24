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

namespace EasyGift_API.Repository
{
    public class ProductSuggestionRepository : Repository<ProductSuggestion>, IProductSuggestionRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductSuggestionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<dynamic> GetProductSuggestions(int id)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetProductSuggestions", connection))
                {
                    cmd.Parameters.AddWithValue("@prod_id", id);

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
                            data["SuggestedFor"] = (string)reader["SuggestedFor"];
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
