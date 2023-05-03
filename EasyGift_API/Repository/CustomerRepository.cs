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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {

            entity.UpdatedDate = DateTime.Now;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public async Task<dynamic> GetRecentNewCustomer()
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("RecentNewCustomersforChart", connection))
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
                            data["Total"] = (int)reader["total"];
                            data["Date"] = (DateTime)reader["add_date"];
                            datas.Add(data);
                        }
                    }
                }
                connection.Close();
            }
            return datas;
        }

        public async Task<List<dynamic>> GetAllCustomers()
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCustomers", connection))
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

        public async Task<List<dynamic>> GetUserReport(int status)
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserReport", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", status);

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

