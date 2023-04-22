using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Models.Dto.Get;
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
    public class SellerRepository : Repository<Seller>, ISellerRepository
    {
        private readonly ApplicationDbContext _db;
        public SellerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Seller> UpdateAsync(Seller entity)
        {

            entity.UpdateDate = DateTime.Now;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Dictionary<string,object>> CreateSellerAsync(CreateNewSellerDTO entity)
        {
            var dict = new Dictionary<string,object>();
            try
            {
                using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("Create_Seller", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SellerFirstName", entity.SellerName);
                        cmd.Parameters.AddWithValue("@SellerLastName", entity.SellerLastName);
                        cmd.Parameters.AddWithValue("@SellerPhoneNo", entity.SellerPhoneNo);
                        cmd.Parameters.AddWithValue("@sellerEmailid", entity.SellerEmail);
                        cmd.Parameters.AddWithValue("@SellerPassword", entity.SellerPassword);
                        cmd.Parameters.AddWithValue("@SellerPancardNo", entity.SellerPancardNo);
                        cmd.Parameters.AddWithValue("@SellerImage", entity.SellerImage);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }
                        //cmd.ExecuteNonQuery();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                int result = (int)reader["Result"];  //reader.GetString(reader.GetOrdinal("Result"));
                                dict["Id"] = result;
                                break;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                var error = new Dictionary<string, object>();
                error["StatusCode"] = 500;
                error["error"] = ex.Message;
                return error;

            }
            var json = JsonConvert.SerializeObject(entity);

            var modelDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            modelDict["Id"] = dict["Id"];

            return modelDict;
        }

        public async Task<List<NewSellerDTO>> GetNewSellers()
        {
            var sellers = new List<NewSellerDTO>();
           
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Get_New_Sellers", connection))
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
                            NewSellerDTO newSellerDTO = new NewSellerDTO();
                            newSellerDTO.SellerName = (string)reader["SellerName"];
                            newSellerDTO.SellerLastName = (string)reader["SellerLastName"];
                            newSellerDTO.ShopName =  reader["ShopName"].ToString();
                            newSellerDTO.EmailId = reader["EmailId"].ToString();
                            newSellerDTO.SellerStatus = (int)reader["SellerStatus"];
                            newSellerDTO.SellerImage = reader["SellerImage"].ToString();
                            sellers.Add(newSellerDTO);
                        }
                    }
                }
                connection.Close();
            }
            return sellers;
        }



        public async Task<dynamic> GetRecentNewSeller()
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("RecentNewSellersforChart", connection))
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

        public async Task<List<dynamic>> GetAllSeller()
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllSellers", connection))
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

        public async Task<dynamic> GetSellerDetails(int id)
        {
            dynamic data = new ExpandoObject();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetCompleteSellerDetail", connection))
                {
                    cmd.Parameters.AddWithValue("@seller_id", id);

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
    }
}
