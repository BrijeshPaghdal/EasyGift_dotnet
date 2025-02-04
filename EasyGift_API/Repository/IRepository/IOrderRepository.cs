﻿using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<dynamic> GetPastOrder(int shopId=0,int limit=7);
        

    }
}
