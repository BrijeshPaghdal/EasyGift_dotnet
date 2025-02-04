﻿using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IUserOnlineRepository : IRepository<UserOnline>
    {
        Task<List<dynamic>> CheckUserIsOnline(Dictionary<string, object> status);
    }
}
