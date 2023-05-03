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
    public class BankDetailsRepository : Repository<BankDetails>, IBankDetailsRepository
    {
        private readonly ApplicationDbContext _db;
       public BankDetailsRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
       
        public async Task<BankDetails> UpdateAsync(BankDetails entity)
        {
            _db.BankDetails.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<dynamic> GetDistinctBankCountry()
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                var sql = "SELECT DISTINCT BankCountry FROM BankDetails where BankName <> ''";
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (await reader.ReadAsync())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data["BankCountry"] = (string)reader["BankCountry"];
                        datas.Add(data);
                    }
                }

                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetDistinctBankStates(string BankCountry)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                var sql = "SELECT DISTINCT BankState FROM BankDetails where BankName <> '' and BankCountry = '"+ BankCountry +"'";
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (await reader.ReadAsync())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data["BankState"] = (string)reader["BankState"];
                        datas.Add(data);
                    }
                }

                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetDistinctBankCities(string BankState)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                var sql = "SELECT DISTINCT BankCity FROM BankDetails where BankName <> '' and BankState = '" + BankState + "'";
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (await reader.ReadAsync())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data["BankCity"] = (string)reader["BankCity"];
                        datas.Add(data);
                    }
                }

                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetDistinctBankNames(string BankCity)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                var sql = "SELECT DISTINCT BankName FROM BankDetails where BankName <> '' and BankCity = '" + BankCity + "'";
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (await reader.ReadAsync())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data["BankName"] = (string)reader["BankName"];
                        datas.Add(data);
                    }
                }

                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetDistinctBankBranch(string BankState, string BankCity, string BankName)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                var sql = "SELECT DISTINCT BankBranch FROM BankDetails where BankName <> '' and BankState = '"+BankState+"' and BankName = '" + BankName + "' and BankCity = '"+ BankCity +"'";
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (await reader.ReadAsync())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data["BankBranch"] = (string)reader["BankBranch"];
                        datas.Add(data);
                    }
                }

                connection.Close();
            }
            return datas;
        }

        public async Task<dynamic> GetDistinctBankDetails(string BankName,string BankBranch)
        {
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                var sql = "SELECT DISTINCT BankIFSC, BankAddress FROM BankDetails where BankName <> '' and BankName = '" + BankName + "' and BankBranch = '" + BankBranch + "'";
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (await reader.ReadAsync())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data["BankIFSC"] = (string)reader["BankIFSC"];
                        data["BankAddress"] = (string)reader["BankAddress"];
                        datas.Add(data);
                    }
                }

                connection.Close();
            }
            return datas;
        }

    }
}
