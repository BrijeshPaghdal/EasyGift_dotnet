using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyGift_API.Repository
{
    public class SellerOnlineRepository : Repository<SellerOnline>, ISellerOnlineRepository
    {
        private readonly ApplicationDbContext _db;
        public SellerOnlineRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<List<dynamic>> CheckSellerIsOnline(Dictionary<string,object> status)
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CheckSellerIsOnline", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@session_id", status["session_id"]);
                    cmd.Parameters.AddWithValue("@session_time", status["session_time"]);

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
