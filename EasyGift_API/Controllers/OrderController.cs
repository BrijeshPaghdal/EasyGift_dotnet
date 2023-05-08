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
namespace EasyGift_API.Controllers
{
    public class OrderController : GenericController<Order, OrderDTO, CreateOrderDTO, UpdateOrderDTO>
    {
        private readonly IOrderRepository _db;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public OrderController(IOrderRepository db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet("GetPastOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetPastOrder([FromQuery] int ShopId = 0, [FromQuery] int limit = 7)
        {
            try
            {
                
                dynamic Order = await _db.GetPastOrder(ShopId,limit);

                if (Order == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Error Occured" };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<Order>.ResponseBody(HttpStatusCode.OK, false, Result: Order));
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