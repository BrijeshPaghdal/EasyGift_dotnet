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
    public class SuggestionController : GenericController<Suggestion, SuggestionDTO, CreateSuggestionDTO, UpdateSuggestionDTO>
    {
        private readonly ISuggestionRepository _dbSuggestion;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public SuggestionController(ISuggestionRepository dbSuggestion, IMapper mapper) : base(dbSuggestion, mapper)
        {
            _dbSuggestion = dbSuggestion;
            _mapper = mapper;
            _response = new APIResponse();
        }

    }
}