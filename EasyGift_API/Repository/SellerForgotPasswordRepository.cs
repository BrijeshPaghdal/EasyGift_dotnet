﻿using AutoMapper;
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
    public class SellerForgotPasswordRepository : Repository<SellerForgotPassword>, ISellerForgotPasswordRepository
    {
        private readonly ApplicationDbContext _db;
        public SellerForgotPasswordRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
