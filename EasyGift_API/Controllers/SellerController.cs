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
using System.Collections.Generic;
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

        [HttpGet("GetNewSellers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetNewSellers()
        {
            try
            {
                IEnumerable<NewSellerDTO> datas;
                datas = await _dbSeller.GetNewSellers();
                if (datas.Count() == 0)
                {
                    return NotFound(CustomMethods<NewSellerDTO>.ResponseBody(HttpStatusCode.NotFound, false));
                }
                _response.Result = _mapper.Map<List<NewSellerDTO>>(datas);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            return _response;
        }

        [HttpGet("GetRecentNewSeller")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetRecentNewSeller()
        {
            try
            {

                dynamic datas = await _dbSeller.GetRecentNewSeller();

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Seller>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("GetAllSeller")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllSeller()
        {
            try
            {

                dynamic datas = await _dbSeller.GetAllSeller();

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Seller>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("GetSellerDetails/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetSellerDetails(int id)
        {
            try
            {

                dynamic datas = await _dbSeller.GetSellerDetails(id);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Seller>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }


        [HttpGet("GetSellerReport/{status:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetSellerReport(int status=1)
        {
            try
            {

                dynamic datas = await _dbSeller.GetSellerReport(status);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Customer>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}