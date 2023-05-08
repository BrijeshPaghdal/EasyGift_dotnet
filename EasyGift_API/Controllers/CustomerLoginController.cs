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
    public class CustomerLoginController : GenericController<CustomerLogin, CustomerLoginDTO, CreateCustomerLoginDTO, UpdateCustomerLoginDTO>
    {
        private readonly ICustomerLoginRepository _db;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public CustomerLoginController(ICustomerLoginRepository db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO loginModel)
        {
            try
            {
                if (loginModel == null)
                    return CustomMethods<CustomerLogin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: loginModel);


                var login = await _db.Login(loginModel);

                if (login.User == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Email or Password is incorrect." };
                    return BadRequest(_response);
                }

                return Ok(CustomMethods<CustomerLogin>.ResponseBody(HttpStatusCode.OK, false, Result: login));
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