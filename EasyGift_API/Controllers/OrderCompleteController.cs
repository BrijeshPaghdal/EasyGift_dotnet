﻿using AutoMapper;
using Azure;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Models.Dto.Get;
using EasyGift_API.Models.Dto.Update;
using EasyGift_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
namespace EasyGift_API.Controllers
{
    public class OrderCompleteController : GenericController<OrderComplete, OrderCompleteDTO, CreateOrderCompleteDTO, UpdateOrderCompleteDTO>
    {
        private readonly IOrderCompleteRepository _db;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public OrderCompleteController(IOrderCompleteRepository db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new APIResponse();
        }

      
    }
}