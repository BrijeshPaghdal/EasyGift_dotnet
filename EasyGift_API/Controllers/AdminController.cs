using AutoMapper;
using Azure;
using EasyGift_API.Controllers.CustomMethod;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Models.Dto.Get;
using EasyGift_API.Models.Dto.Update;
using EasyGift_API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace EasyGift_API.Controllers
{
    //[Route("api/EasyGift/Admin")]
    //[ApiController]
    public class AdminController : GenericController<Admin, AdminDTO, CreateAdminDTO, UpdateAdminDTO>
    {
        private readonly IAdminRepository _dbAdmin;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public AdminController(IAdminRepository dbAdmin, IMapper mapper) : base(dbAdmin, mapper)
        {
            _dbAdmin = dbAdmin;
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
                    return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: loginModel);


                var login = await _dbAdmin.Login(loginModel);

                if (login.User == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "Email or Password is incorrect."};
                    return BadRequest(_response);
                }
                
                return Ok(CustomMethods<Admin>.ResponseBody(HttpStatusCode.OK, false, Result: login));
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