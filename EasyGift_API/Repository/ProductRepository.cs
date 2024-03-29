﻿using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Dynamic;
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
        public async Task<dynamic> GetPastAddedProduct(int shopId = 0, int limit = 7)
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



        public async Task<dynamic> GetProductDetail(int id)
        {
            dynamic data = new ExpandoObject();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetCompleteProductDetail", connection))
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

        public async Task<List<dynamic>> GetFilteredProducts(Filter filter)
        {
            List<dynamic> datas = new List<dynamic>();
            using (SqlConnection connection = new SqlConnection(StoredConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetFilteredProducts", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@city_name", filter.CityName);
                    cmd.Parameters.AddWithValue("@minimum_price", filter.MinPrice);
                    cmd.Parameters.AddWithValue("@maximum_price", filter.MaxPrice);
                    cmd.Parameters.AddWithValue("@category_name", filter.CategoryName);
                    cmd.Parameters.AddWithValue("@subcategory_name", filter.SubCategoryName);
                    cmd.Parameters.AddWithValue("@category_ids", filter.CategoryIds);
                    cmd.Parameters.AddWithValue("@suggestion_ids", filter.SuggestionIds);

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
