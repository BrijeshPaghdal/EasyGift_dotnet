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
    public class ShopController : GenericController<Shop, ShopDTO, CreateShopDTO, UpdateShopDTO>
    {
        private readonly IShopRepository _dbShop;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public ShopController(IShopRepository dbShop, IMapper mapper) : base(dbShop, mapper)
        {
            _dbShop = dbShop;
            _mapper = mapper;
            _response = new APIResponse();
        }
        [HttpGet("GetTotalOrder/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetTotalOrder(int id)
        {
            try
            {

                dynamic datas = await _dbShop.GetTotalOrder(id);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Shop>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("GetTotalIncome/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetTotalIncome(int id)
        {
            try
            {

                dynamic datas = await _dbShop.GetTotalIncome(id);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Shop>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("GetTotalIncomeforChart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetTotalIncomeforChart([FromQuery] int ShopId, [FromQuery] int limit = 7)
        {
            try
            {

                dynamic lastTotalIncome = await _dbShop.GetTotalIncomeforChart(ShopId, limit);

                if (lastTotalIncome == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Shop>.ResponseBody(HttpStatusCode.OK, false, Result: lastTotalIncome));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }


        [HttpGet("{id:int}/GetTotalProductSold")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetTotalProductSold(int id)
        {
            try
            {

                dynamic datas = await _dbShop.GetTotalProductSold(id);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Shop>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("{id:int}/GetTotalProductSoldforChart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetTotalProductSoldforChart(int id, [FromQuery] int limit = 7)
        {
            try
            {

                dynamic lastTotalIncome = await _dbShop.GetTotalProductSoldforChart(id, limit);

                if (lastTotalIncome == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Shop>.ResponseBody(HttpStatusCode.OK, false, Result: lastTotalIncome));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("{id:int}/GetOrders/{status:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetOrders(int id = 0,int status=3, [FromQuery] int limit=0)
        {
            try
            {

                dynamic datas = await _dbShop.GetOrders(id,status,limit);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Order>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("{ShopId:int}/CompleteOrder/{OrderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CompleteOrder(int ShopId , int OrderId)
        {
            try
            {

                dynamic datas = await _dbShop.CompleteOrder(ShopId, OrderId);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Order>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpGet("{id:int}/GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetProducts(int id = 0, [FromQuery]int Status=3)
        {
            try
            {

                dynamic datas = await _dbShop.GetProducts(id,Status);

                if (datas == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Product>.ResponseBody(HttpStatusCode.OK, false, Result: datas));
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