using AutoMapper;
using Azure;
using EasyGift_API.Controllers.CustomMethod;
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
    [Route("api/EasyGift/Seller")]
    [ApiController]
    public class SellerController : GenericController<Seller, SellerDTO, CreateSellerDTO, UpdateSellerDTO>
    {
        private readonly ISellerRepository _dbSeller;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public SellerController(ISellerRepository dbSeller, IMapper mapper) : base(dbSeller, mapper)
        {
            _dbSeller = dbSeller;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpPost("CreateNewSeller")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> InsertData([FromBody] CreateNewSellerDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                    return BadRequest(CustomMethods<Seller>.ResponseBody(HttpStatusCode.BadRequest, false, Result: createDTO));

                var model = await _dbSeller.CreateSellerAsync(createDTO);
                if(model.ContainsKey("StatusCode") && (int)model["StatusCode"] == 500)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.ErrorsMessages =new List<string>() { (string)model["error"] } ;
                    return BadRequest(_response);
                }
                dynamic result = CustomMethods<Seller>.ConvertDictionaryToObject<Seller>(model);
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.Created;
                var Id = result.Id;
                return CreatedAtAction("GetDataById", new { id = Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }
    }
}