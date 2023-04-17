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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyGift_API.Controllers
{
    //[Route("api/EasyGift/BankDetail")]
    //[ApiController]
    public class BankDetailsController : GenericController<BankDetails, BankDetailsDTO, CreateBankDetailsDTO, UpdateBankDetailsDTO>
    {
        private readonly IBankDetailsRepository _dbBankDetail;
        private readonly IMapper _mapper;

        public BankDetailsController(IBankDetailsRepository dbBankDetail, IMapper mapper) : base(dbBankDetail, mapper)
        {
            _dbBankDetail = dbBankDetail;
            _mapper = mapper;
        }

    }
}
