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

    }
}
