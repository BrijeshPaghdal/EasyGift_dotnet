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
    [Route("api/EasyGift/BankDetail")]
    [ApiController]
    public class BankDetailsController : ControllerBase
    {
        private readonly IBankDetailsRepository _dbBankDetail;
        private readonly IMapper _mapper;

        public BankDetailsController(IBankDetailsRepository dbBankDetail,IMapper mapper)
        {
            _dbBankDetail = dbBankDetail;
            _mapper= mapper;
        }
        
        //Http Requests

        [HttpGet(Name = "GetBankDetails")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetBankDetails([FromQuery] string[] columns, [FromQuery] int limit=50)
        {
            IEnumerable<BankDetails> bankDetails;
            if (limit >0)
            {
                bankDetails = await _dbBankDetail.GetAllAsync(limit:limit);
            }
            else
            {
                bankDetails = await _dbBankDetail.GetAllAsync();
            }
            if (columns.Length != 0)
            {
                List<Dictionary<string, object>> fetchedBankDetails = new List<Dictionary<string, object>>();
                foreach(var BankDetail in bankDetails)
                {
                    var response = CustomMethods.fetchPerticularColumns(columns, BankDetail);
                    if (response.ContainsKey("Error"))
                    {
                        return BadRequest(response["Error"]);
                    }
                   fetchedBankDetails.Add(response);
                }
                return Ok(fetchedBankDetails);
            }
            return Ok(_mapper.Map<List<BankDetailsDTO>>(bankDetails));
        }

     
        [HttpGet("{id:int}", Name = "GetBankDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string,object>>> GetBankDetail(int id,[FromQuery] string[] columns)
        {
            if (id == 0)
                return BadRequest();
            var bankDetails = await _dbBankDetail.GetAsync(u => u.BankId == id);

            if (bankDetails == null)
                return NotFound();
            BankDetailsDTO model = _mapper.Map<BankDetailsDTO>(bankDetails);

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


        [HttpPost(Name = "CreateBankDetail")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateBankDetailsDTO>> CreateBankDetail([FromBody] CreateBankDetailsDTO createBankDetailDTO)
        {
            if (createBankDetailDTO == null)
                return BadRequest(createBankDetailDTO);
            BankDetails model = _mapper.Map<BankDetails>(createBankDetailDTO);
            await _dbBankDetail.CreateAsync(model);
            return CreatedAtRoute("GetBankDetailsById", new { id = model.BankId }, model);
        }

        [HttpPatch("{id:int}", Name = "UpdateBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateBankDetail(int id, [FromBody] Dictionary<string, object> patchDTO){
            if(patchDTO == null || id==0)
                return BadRequest();
            var bankDetail =await _dbBankDetail.GetAsync(u => u.BankId == id,tracked:false);
            if(bankDetail == null)
                return NotFound();

            foreach (var update in patchDTO)
            {
                var property = bankDetail.GetType().GetProperty(update.Key);

                if (property != null)
                {
                    var convertedValue = Convert.ChangeType(update.Value, property.PropertyType);
                    property.SetValue(bankDetail, convertedValue);
                }
                else
                {
                    return BadRequest($"Invalid property name: {update.Key}");
                }
            }

            UpdateBankDetailsDTO updatedBankDetail = _mapper.Map<UpdateBankDetailsDTO>(bankDetail);
            BankDetails model = _mapper.Map<BankDetails>(updatedBankDetail);

            await _dbBankDetail.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetBankDetailsById", new { id = model.BankId }, model);

        }

        [HttpDelete("{id:int}", Name = "DeleteBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBankDetail(int id)
        {
            if(id == 0)
                return BadRequest();
            var bankDetail = await _dbBankDetail.GetAsync(u=>u.BankId == id);
            if(bankDetail == null) return NotFound();
            await _dbBankDetail.RemoveAsync(bankDetail);
            Dictionary<string,object> responseSuccess= new Dictionary<string, object>();
            responseSuccess.Add("message","Record Deleted Successfully");
            return Ok(responseSuccess);
        }
    }
}
