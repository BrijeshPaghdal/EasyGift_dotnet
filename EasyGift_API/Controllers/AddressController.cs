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
    //[Route("api/EasyGift/Address")]
    //[ApiController]
    public class AddressController : GenericController<Address, AddressDTO, CreateAddressDTO, UpdateAddressDTO>
    {
        protected APIResponse _response;
        private readonly IAddressRepository _dbAddress;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository dbAddress, IMapper mapper) : base(dbAddress, mapper)
        {
            _dbAddress = dbAddress;
            _mapper = mapper;
            this._response = new();
        }

        ////Http Requests

        //[HttpGet(Name = "GetAddresses")]
        //public async Task<ActionResult<APIResponse>> GetAddresses([FromQuery] string[] columns, [FromQuery] string? filter = null)
        //{
        //    try
        //    {
        //        IEnumerable<Address> addresses;
        //        if (filter != null)
        //        {
        //            var Filter = CustomMethods<Address>.ConvertToExpression<Address>(filter);
        //            addresses = await _dbAddress.GetAllAsync(filter: Filter);
        //        }
        //        else
        //        {
        //            addresses = await _dbAddress.GetAllAsync();
        //        }
        //        if (addresses.Count() == 0)
        //            return CustomMethods<Address>.ResponseBody(HttpStatusCode.NotFound, false);
        //        if (columns.Length != 0)
        //        {
        //            List<Dictionary<string, object>> fetchedAddresses = new List<Dictionary<string, object>>();
        //            foreach (var address in addresses)
        //            {
        //                var response = CustomMethods<Address>.fetchPerticularColumns(columns, address);
        //                if (response.ContainsKey("Error"))
        //                {
        //                    return CustomMethods<Address>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]);
        //                }
        //                fetchedAddresses.Add(response);
        //            }
        //            _response.Result = fetchedAddresses;
        //        }
        //        else
        //        {
        //            _response.Result = _mapper.Map<List<AddressDTO>>(addresses);
        //        }
        //        _response.StatusCode = HttpStatusCode.OK;

        //        return _response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}


        //[HttpGet("{id:int}", Name = "GetAddressById")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<APIResponse>> GetAddress(int id, [FromQuery] string[] columns, [FromQuery] string? filter = null)
        //{
        //    try
        //    {
        //        if (id == 0)
        //            return CustomMethods<Address>.ResponseBody(HttpStatusCode.BadRequest, false);

        //        Address address = new Address();
        //        if (filter != null)
        //        {
        //            var Filter = CustomMethods<Address>.ConvertToExpression<Address>(filter + "&& u => u.AddressId ==" + id);
        //            address = await _dbAddress.GetAsync(filter: Filter);
        //        }
        //        else
        //        {
        //            address = await _dbAddress.GetAsync(u => u.AddressId == id);
        //        }

        //        if (address == null)
        //            return CustomMethods<Address>.ResponseBody(HttpStatusCode.NotFound, false);
        //        AddressDTO model = _mapper.Map<AddressDTO>(address);

        //        if (columns.Length != 0)
        //        {
        //            var response = CustomMethods<Address>.fetchPerticularColumns(columns, model);
        //            if (response.ContainsKey("Error"))
        //            {
        //                return CustomMethods<Address>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]);
        //            }
        //            _response.Result = response;
        //        }
        //        else
        //        {
        //            _response.Result = _mapper.Map<List<AddressDTO>>(model); ;
        //        }

        //        _response.StatusCode = HttpStatusCode.OK;

        //        return Ok(_response);
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
        //public async Task<ActionResult<APIResponse>> CreateAddress([FromBody] CreateAddressDTO createAddressDTO)
        //{
        //    try
        //    {
        //        if (createAddressDTO == null)
        //            return CustomMethods<Address>.ResponseBody(HttpStatusCode.BadRequest, false, Result: createAddressDTO);
        //        Address model = _mapper.Map<Address>(createAddressDTO);
        //        await _dbAddress.CreateAsync(model);

        //        _response.Result = _mapper.Map<List<AddressDTO>>(model); ;
        //        _response.StatusCode = HttpStatusCode.Created;
        //        _response.IsSuccess = true;
        //        return CreatedAtRoute("GetAddressById", new { id = model.AddressId }, _response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}

        //[HttpPatch("{id:int}", Name = "UpdateAddress")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public async Task<ActionResult<APIResponse>> UpdateAddress(int id, [FromBody] Dictionary<string, object> patchDTO)
        //{
        //    try
        //    {
        //        if (patchDTO == null || id == 0)
        //            return CustomMethods<Address>.ResponseBody(HttpStatusCode.BadRequest, false);
        //        var address = await _dbAddress.GetAsync(u => u.AddressId == id, tracked: false);
        //        if (address == null)
        //            return CustomMethods<Address>.ResponseBody(HttpStatusCode.NotFound, false);

        //        foreach (var update in patchDTO)
        //        {
        //            var property = address.GetType().GetProperty(update.Key);

        //            if (property != null)
        //            {
        //                var convertedValue = Convert.ChangeType(update.Value, property.PropertyType);
        //                property.SetValue(address, convertedValue);
        //            }
        //            else
        //            {
        //                return CustomMethods<Address>.ResponseBody(HttpStatusCode.BadRequest, false, ErrorMessages: new List<string> { $"Invalid property name: {update.Key}" });
        //            }
        //        }

        //        UpdateAddressDTO updatedAddress = _mapper.Map<UpdateAddressDTO>(address);
        //        Address model = _mapper.Map<Address>(updatedAddress);

        //        await _dbAddress.UpdateAsync(model);
        //        if (!ModelState.IsValid)
        //        {
        //            return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: ModelState);
        //        }
        //        return CreatedAtRoute("GetAddressById", new { id = model.AddressId }, model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorsMessages = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}

        //[HttpDelete("{id:int}", Name = "DeleteAddress")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<APIResponse>> DeleteAddress(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //            return CustomMethods<Address>.ResponseBody(HttpStatusCode.BadRequest, false);
        //        var address = await _dbAddress.GetAsync(u => u.AddressId == id);
        //        if (address == null) return CustomMethods<Address>.ResponseBody(HttpStatusCode.NotFound, false);
        //        await _dbAddress.RemoveAsync(address);

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
