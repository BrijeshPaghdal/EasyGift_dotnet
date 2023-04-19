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
    public class StatesController : GenericController<States, StatesDTO, CreateStatesDTO, UpdateStatesDTO>
    {
        private readonly IStatesRepository _dbStates;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public StatesController(IStatesRepository dbStates, IMapper mapper) : base(dbStates, mapper)
        {
            _dbStates = dbStates;
            _mapper = mapper;
            _response = new APIResponse();
        }

    }
}