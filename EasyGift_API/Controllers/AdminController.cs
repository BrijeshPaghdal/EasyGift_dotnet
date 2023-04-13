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
//brijesh changes
namespace EasyGift_API.Controllers
{
    [Route("api/EasyGift/Admin")]
    [ApiController]
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

        //Http Requests

        //[HttpGet(Name = "GetAdmins")]
        //public async Task<ActionResult<APIResponse>> GetAdmins([FromQuery] string[] columns, [FromQuery] string? filter = null)
        //{
        //    try
        //    {
        //        IEnumerable<Admin> admins;
        //        if (filter != null)
        //        {
        //            var Filter = CustomMethods<Admin>.ConvertToExpression<Admin>(filter);
        //            admins = await _dbAdmin.GetAllAsync(filter: Filter);
        //        }
        //        else
        //        {
        //            admins = await _dbAdmin.GetAllAsync();
        //        }
        //        if (admins.Count() == 0)
        //        {
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.NotFound, false);
        //        }
        //        if (columns.Length != 0)
        //        {
        //            List<Dictionary<string, object>> fetchedAdmins = new List<Dictionary<string, object>>();
        //            foreach (var admin in admins)
        //            {
        //                var response = CustomMethods<Admin>.fetchPerticularColumns(columns, admin);
        //                if (response.ContainsKey("Error"))
        //                {
        //                    return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]);
        //                }
        //                fetchedAdmins.Add(response);
        //            }
        //            _response.Result = fetchedAdmins;
        //        }
        //        else
        //        {
        //            _response.Result = _mapper.Map<List<AdminDTO>>(admins);
        //        }
        //        _response.StatusCode = HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}


        //[HttpGet("{id:int}", Name = "GetAdminById")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<APIResponse>> GetAdmin(int id, [FromQuery] string[] columns, [FromQuery] string? filter = null)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false);
        //        }
        //        Admin admin = new Admin();
        //        if (filter != null)
        //        {
        //            var Filter = CustomMethods<Admin>.ConvertToExpression<Admin>(filter + "&& u => u.Id ==" + id);
        //            admin = await _dbAdmin.GetAsync(filter: Filter);
        //        }
        //        else
        //        {
        //            admin = await _dbAdmin.GetAsync(u => u.Id == id);
        //        }
        //        if (admin == null)
        //        {
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.NotFound, false);
        //        }
        //        AdminDTO model = _mapper.Map<AdminDTO>(admin);

        //        if (columns.Length != 0)
        //        {
        //            var response = CustomMethods<Admin>.fetchPerticularColumns(columns, model);
        //            if (response.ContainsKey("Error"))
        //            {
        //                return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]);
        //            }
        //            _response.Result = response;
        //        }
        //        else
        //        {
        //            _response.Result = _mapper.Map<List<AdminDTO>>(model); ;
        //        }
        //        _response.StatusCode = HttpStatusCode.OK;

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}


        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> CreateAdmin([FromBody] CreateAdminDTO createAdminDTO)
        //{
        //    try
        //    {
        //        if (createAdminDTO == null)
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: createAdminDTO);
        //        Admin model = _mapper.Map<Admin>(createAdminDTO);
        //        await _dbAdmin.CreateAsync(model);

        //        _response.Result = _mapper.Map<List<AdminDTO>>(model); ;
        //        _response.StatusCode = HttpStatusCode.Created;
        //        _response.IsSuccess = true;
        //        return CreatedAtRoute("GetAdminById", new { id = model.Id }, _response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}

        //[HttpPatch("{id:int}", Name = "UpdateAdmin")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public async Task<ActionResult<APIResponse>> UpdateAdmin(int id, [FromBody] Dictionary<string, object> patchDTO)
        //{
        //    try
        //    {
        //        if (patchDTO == null || id == 0)
        //        {
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false);
        //        }
        //        var Admin = await _dbAdmin.GetAsync(u => u.Id == id, tracked: false);
        //        if (Admin == null)
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.NotFound, false);

        //        foreach (var update in patchDTO)
        //        {
        //            var property = Admin.GetType().GetProperty(update.Key);

        //            if (property != null)
        //            {
        //                var convertedValue = Convert.ChangeType(update.Value, property.PropertyType);
        //                property.SetValue(Admin, convertedValue);
        //            }
        //            else
        //            {
        //                return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, ErrorMessages: new List<string> { $"Invalid property name: {update.Key}" });
        //            }
        //        }

        //        UpdateAdminDTO updatedAdmin = _mapper.Map<UpdateAdminDTO>(Admin);
        //        Admin model = _mapper.Map<Admin>(updatedAdmin);

        //        await _dbAdmin.UpdateAsync(model);
        //        if (!ModelState.IsValid)
        //        {
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: ModelState);
        //        }
        //        return CreatedAtRoute("GetAdminById", new { id = model.Id }, model);
        //        //return CreatedAtRoute("GetAdminById", new { id = model.GetType().GetProperty("Id").GetValue(model, null) }, model);

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}

        //[HttpDelete("{id:int}", Name = "DeleteAdmin")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<APIResponse>> DeleteAdmin(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false);

        //        var admin = await _dbAdmin.GetAsync(u => u.Id == id);
        //        if (admin == null) return CustomMethods<Admin>.ResponseBody(HttpStatusCode.NotFound, false);
        //        await _dbAdmin.RemoveAsync(admin);

        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.Result = "Record Deleted Successfully";

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}
    }
}
//changes