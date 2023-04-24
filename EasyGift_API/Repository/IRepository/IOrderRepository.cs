﻿using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<dynamic> GetPastWeekOrder();
        Task<dynamic> GetOrders(int id);

    }
}