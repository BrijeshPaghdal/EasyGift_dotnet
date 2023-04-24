﻿using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<dynamic> GetProductReviews(int id, int limit = 5);
    }
}