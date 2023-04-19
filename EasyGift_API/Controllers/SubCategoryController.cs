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
    public class SubCategoryController : GenericController<SubCategory, SubCategoryDTO, CreateSubCategoryDTO, UpdateSubCategoryDTO>
    {
        private readonly ISubCategoryRepository _dbSubCategory;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public SubCategoryController(ISubCategoryRepository dbSubCategory, IMapper mapper) : base(dbSubCategory, mapper)
        {
            _dbSubCategory = dbSubCategory;
            _mapper = mapper;
            _response = new APIResponse();
        }

    }
}