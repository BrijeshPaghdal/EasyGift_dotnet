using AutoMapper;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace EasyGift_API.Controllers
{
    public class SellerOnlineController : GenericController<SellerOnline, SellerOnlineDTO, CreateSellerOnlineDTO, UpdateSellerOnlineDTO>
    {
        private readonly ISellerOnlineRepository _dbSellerOnline;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public SellerOnlineController(ISellerOnlineRepository dbSellerOnline, IMapper mapper) : base(dbSellerOnline, mapper)
        {
            _dbSellerOnline = dbSellerOnline;
            _mapper = mapper;
            _response = new APIResponse();
        }

    }
}