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
    public class ShopRepository : Repository<Shop>, IShopRepository
    {
        private readonly ApplicationDbContext _db;
        public ShopRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<dynamic> GetTotalOrder(int id)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetTotalOrderOfSeller", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@shop_id", id);

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
                            data["total_order"] = (int)reader["total_order"];
                            datas.Add(data);
                        }
                    }
                }
                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetTotalIncome(int shopId)
        {
            dynamic data = new ExpandoObject();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetTotalIncome", connection))
                {
                    cmd.Parameters.AddWithValue("@shop_id", shopId);

                    cmd.CommandType = CommandType.StoredProcedure;
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    //cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        var dict = data as IDictionary<string, object>;
                        while (await reader.ReadAsync())
                        {

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string name = reader.GetName(i);
                                object value = reader.GetValue(i);

                                dict.Add(name, value);
                            }

                        }
                    }
                }
                connection.Close();
            }
            return data;
        }


        public async Task<dynamic> GetTotalIncomeforChart(int shopId, int limit = 7)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetTotalIncomeforChart", connection))
                {
                    cmd.Parameters.AddWithValue("@shop_id", shopId);
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
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data["Total"] = (int)reader["Total"];
                            data["Date"] = (string)reader["OrderDate"];
                            datas.Add(data);
                        }
                    }
                }
                connection.Close();
            }
            return datas;
        }


        public async Task<dynamic> GetTotalProductSold(int id)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetTotalProductSold", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@shop_id", id);

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
                            data["Total"] = (int)reader["Total"];
                            datas.Add(data);
                        }
                    }
                }
                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetTotalProductSoldforChart(int shopId, int limit = 7)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetTotalProductSoldforChart", connection))
                {
                    cmd.Parameters.AddWithValue("@shop_id", shopId);
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
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data["Total"] = (int)reader["Total"];
                            data["ProductId"] = (int)reader["ProductId"];
                            data["ProductName"]=(string)reader["ProductName"];
                            datas.Add(data);
                        }
                    }
                }
                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> CompleteOrder(int shopId, int orderId)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CompleteOrder", connection))
                {
                    cmd.Parameters.AddWithValue("@ShopId", shopId);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);


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
                            data["Result"] = (string)reader["Result"];
                            datas.Add(data);
                        }
                    }
                }
                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetOrders(int id = 0, int status=3,int limit=0)
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetOrders", connection))
                {
                    cmd.Parameters.AddWithValue("@shop_id", id);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    cmd.Parameters.AddWithValue("@status", status);

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

        public async Task<List<dynamic>> GetProducts(int id = 0,int Status=3,int limit=0)
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetProducts", connection))
                {
                    cmd.Parameters.AddWithValue("@shop_id", id);
                    cmd.Parameters.AddWithValue("@prod_status", Status);
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
