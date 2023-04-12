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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyGift_API.Controllers
{
    [Route("api/EasyGift/Address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _dbAddress;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository dbAddress,IMapper mapper)
        {
            _dbAddress = dbAddress;
            _mapper= mapper;
        }
        
        //Http Requests

        [HttpGet(Name = "GetAddresses")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetAddresses([FromQuery] string[] columns, [FromQuery] string? filter = null)
        {
            IEnumerable<Address> addresses;
            if (filter != null)
            {
                var Filter = CustomMethods<Address>.ConvertToExpression<Address>(filter);
                addresses = await _dbAddress.GetAllAsync(filter: Filter);
            }
            else
            {   
                addresses = await _dbAddress.GetAllAsync();
            }
            if (addresses.Count() == 0)
                return NotFound();
            if (columns.Length != 0)
            {
                List<Dictionary<string, object>> fetchedAddresses = new List<Dictionary<string, object>>();
                foreach(var address in addresses)
                {
                    var response = CustomMethods<Address>.fetchPerticularColumns(columns, address);
                    if (response.ContainsKey("Error"))
                    {
                        return BadRequest(response["Error"]);
                    }
                   fetchedAddresses.Add(response);
                }
                return Ok(fetchedAddresses);
            }
            return Ok(_mapper.Map<List<AddressDTO>>(addresses));
        }

     
        [HttpGet("{id:int}", Name = "GetAddressById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string,object>>> GetAddress(int id,[FromQuery] string[] columns, [FromQuery] string? filter = null)
        {
            if (id == 0)
                return BadRequest();

            Address address = new Address();
            if (filter != null)
            {
                var Filter = CustomMethods<Address>.ConvertToExpression<Address>(filter + "&& u => u.AddressId ==" + id);
                address = await _dbAddress.GetAsync(filter: Filter);
            }
            else
            {
                address = await _dbAddress.GetAsync(u => u.AddressId == id);
            }

            if (address == null)
                return NotFound();
            AddressDTO model = _mapper.Map<AddressDTO>(address);

            if (columns.Length != 0) {
                var response = CustomMethods<Address>.fetchPerticularColumns(columns, model);
                if (response.ContainsKey("Error"))
                {
                    return BadRequest(response["Error"]);
                }
                return Ok(response);
            }
            return Ok(model);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateAddressDTO>> CreateAddress([FromBody] CreateAddressDTO createAddressDTO)
        {
            if (createAddressDTO == null)
                return BadRequest(createAddressDTO);
            Address model = _mapper.Map<Address>(createAddressDTO);
            await _dbAddress.CreateAsync(model);
            return CreatedAtRoute("GetAddressById", new { id = model.AddressId }, model);
        }

        [HttpPatch("{id:int}", Name = "UpdateAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Dictionary<string, object> patchDTO){
            if(patchDTO == null || id==0)
                return BadRequest();
            var address =await _dbAddress.GetAsync(u => u.AddressId == id,tracked:false);
            if(address == null)
                return NotFound();

            foreach (var update in patchDTO)
            {
                var property = address.GetType().GetProperty(update.Key);

                if (property != null)
                {
                    var convertedValue = Convert.ChangeType(update.Value, property.PropertyType);
                    property.SetValue(address, convertedValue);
                }
                else
                {
                    return BadRequest($"Invalid property name: {update.Key}");
                }
            }

            UpdateAddressDTO updatedAddress = _mapper.Map<UpdateAddressDTO>(address);
            Address model = _mapper.Map<Address>(updatedAddress);

            await _dbAddress.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetAddressById", new { id = model.AddressId }, model);

        }

        [HttpDelete("{id:int}", Name = "DeleteAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if(id == 0)
                return BadRequest();
            var address = await _dbAddress.GetAsync(u=>u.AddressId == id);
            if(address == null) return NotFound();
            await _dbAddress.RemoveAsync(address);
            Dictionary<string,object> responseSuccess= new Dictionary<string, object>();
            responseSuccess.Add("message","Record Deleted Successfully");
            return Ok(responseSuccess);
        }
    }
}
