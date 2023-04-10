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
    [Route("api/EasyGift/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _dbAdmin;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository dbAdmin,IMapper mapper)
        {
            _dbAdmin = dbAdmin;
            _mapper= mapper;
        }
        
        //Http Requests

        [HttpGet(Name = "GetAdmins")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetAdmins([FromQuery] string[] columns)
        {
            IEnumerable<Admin> admins = await _dbAdmin.GetAllAsync();
            if (columns.Length != 0)
            {
                List<Dictionary<string, object>> fetchedAdmins = new List<Dictionary<string, object>>();
                foreach(var admin in admins)
                {
                    var response = CustomMethods.fetchPerticularColumns(columns, admin);
                    if (response.ContainsKey("Error"))
                    {
                        return BadRequest(response["Error"]);
                    }
                   fetchedAdmins.Add(response);
                }
                return Ok(fetchedAdmins);
            }
            return Ok(_mapper.Map<List<AdminDTO>>(admins));
        }

     
        [HttpGet("{id:int}", Name = "GetAdminById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string,object>>> GetAdmin(int id,[FromQuery] string[] columns)
        {
            if (id == 0)
                return BadRequest();
            var admins = await _dbAdmin.GetAsync(u => u.AdminId == id);

            if (admins == null)
                return NotFound();
            AdminDTO model = _mapper.Map<AdminDTO>(admins);

            if (columns.Length != 0) {
                var response = CustomMethods.fetchPerticularColumns(columns, model);
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
        public async Task<ActionResult<CreateAdminDTO>> CreateAdmin([FromBody] CreateAdminDTO createAdminDTO)
        {
            if (createAdminDTO == null)
                return BadRequest(createAdminDTO);
            Admin model = _mapper.Map<Admin>(createAdminDTO);
            await _dbAdmin.CreateAsync(model);
            return CreatedAtRoute("GetAdminById", new { id = model.AdminId }, model);
        }

        [HttpPatch("{id:int}", Name = "UpdateAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] Dictionary<string, object> patchDTO){
            if(patchDTO == null || id==0)
                return BadRequest();
            var Admin =await _dbAdmin.GetAsync(u => u.AdminId == id,tracked:false);
            if(Admin == null)
                return NotFound();

            foreach (var update in patchDTO)
            {
                var property = Admin.GetType().GetProperty(update.Key);

                if (property != null)
                {
                    var convertedValue = Convert.ChangeType(update.Value, property.PropertyType);
                    property.SetValue(Admin, convertedValue);
                }
                else
                {
                    return BadRequest($"Invalid property name: {update.Key}");
                }
            }

            UpdateAdminDTO updatedAdmin = _mapper.Map<UpdateAdminDTO>(Admin);
            Admin model = _mapper.Map<Admin>(updatedAdmin);

            await _dbAdmin.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetAdminById", new { id = model.AdminId }, model);

        }

        [HttpDelete("{id:int}", Name = "DeleteAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if(id == 0)
                return BadRequest();
            var Admin = await _dbAdmin.GetAsync(u=>u.AdminId == id);
            if(Admin == null) return NotFound();
            await _dbAdmin.RemoveAsync(Admin);
            Dictionary<string,object> responseSuccess= new Dictionary<string, object>();
            responseSuccess.Add("message","Record Deleted Successfully");
            return Ok(responseSuccess);
        }
    }
}
