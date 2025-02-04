﻿using AutoMapper;
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
using System.Linq.Expressions;
using System.Net;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyGift_API.Controllers
{
    [Route("api/EasyGift/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TEntityDto, TCreateDto, TUpdateDto> : Controller
        where TEntity : class, new()
        where TEntityDto : class, new()
        where TCreateDto : class, new()
        where TUpdateDto : class, new()
    {
        private readonly IRepository<TEntity> _db;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public GenericController(IRepository<TEntity> db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new APIResponse();
        }

        //Http Requests

        [HttpGet(Name = "[controller]/GetDatas")]
        public async Task<ActionResult<APIResponse>> GetDatas([FromQuery] string[] columns, [FromQuery] string? filter = null, [FromQuery] int limit=0)
        {
            try
            {
                IEnumerable<TEntity> datas;
                if (filter != null)
                {
                    var Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>(filter);
                    if(limit>0)
                        datas = await _db.GetAllAsync(filter: Filter,limit:limit);
                    else
                        datas = await _db.GetAllAsync(filter: Filter);
                }
                else
                {
                    if (limit > 0)
                        datas = await _db.GetAllAsync(limit: limit);
                    else
                        datas = await _db.GetAllAsync();
                }
                if (datas.Count() == 0)
                {
                    return NotFound(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false));
                }
                if (columns.Length != 0)
                {
                    List<Dictionary<string, object>> fetchedDatas = new List<Dictionary<string, object>>();
                    foreach (var data in datas)
                    {
                        var response = CustomMethods<Admin>.fetchPerticularColumns(columns, data);
                        if (response.ContainsKey("Error"))
                        {
                            return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]));
                        }
                        fetchedDatas.Add(response);
                    }
                    _response.Result = fetchedDatas;
                }
                else
                {
                    _response.Result = _mapper.Map<List<TEntityDto>>(datas);
                }
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode= HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            return _response;
        }


        [HttpGet("{id:int}", Name ="[controller]/GetDataById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDataById(int id, [FromQuery] string[] columns, [FromQuery] string? filter = null)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false,ErrorMessages:new List<string> {"Id should not be 0"}));
                }
                TEntity entity = new TEntity();
                Expression<Func<TEntity, bool>> Filter;
                if (filter != null)
                {
                    Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>(filter + "&& u => u.Id ==" + id);
                }
                else
                {
                    Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>("u => u.Id ==" + id);
                }
                entity = await _db.GetAsync(filter: Filter);
                //}
                //else
                //{
                //    var Filter = "u => u.Id == id";
                //    entity = await _db.GetAsync(filter: Filter);
                //}
                if (entity == null)
                {
                    return NotFound(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false, ErrorMessages: new List<string> { "Record Not Found" }));
                }
                TEntityDto model = _mapper.Map<TEntityDto>(entity);

                if (columns.Length != 0)
                {
                    var response = CustomMethods<TEntityDto>.fetchPerticularColumns(columns, model);
                    if (response.ContainsKey("Error"))
                    {
                        return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]));
                    }
                    _response.Result = response;
                }
                else
                {
                    _response.Result = _mapper.Map<TEntityDto>(model); ;
                }
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            return Ok(_response);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> InsertData([FromBody] TCreateDto createDTO)
        {
            try
            {
                if (createDTO == null)
                    return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, Result: createDTO));
                TEntity model = _mapper.Map<TEntity>(createDTO);
                await _db.CreateAsync(model);

                dynamic result = _mapper.Map<TEntity>(model);
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                var Id = result.Id;
                return CreatedAtAction("GetDataById", new { id = Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode= HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpPatch("{id:int}", Name = "[controller]/UpdateData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> UpdateData(int id, [FromBody] Dictionary<string, object> patchDTO)
        {
            try
            {
                if (patchDTO == null || id == 0)
                {
                    return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false));
                }
                var Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>("u => u.Id ==" + id);

                var entity = await _db.GetAsync(filter: Filter, tracked: false);
                if (entity == null)
                    return NotFound(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false));

                foreach (var update in patchDTO)
                {
                    var property = entity.GetType().GetProperty(update.Key);

                    if (property != null)
                    {
                        var convertedValue = Convert.ChangeType(update.Value, property.PropertyType);
                        property.SetValue(entity, convertedValue);
                    }
                    else
                    {
                        return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, ErrorMessages: new List<string> { $"Invalid property name: {update.Key}" }));
                    }
                }

                TUpdateDto updatedEntity = _mapper.Map<TUpdateDto>(entity);
                TEntity model = _mapper.Map<TEntity>(updatedEntity);
                await _db.UpdateAsync(model);
                if (!ModelState.IsValid)
                {
                    return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, Result: ModelState));
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                //return CreatedAtRoute("GetDataById", Filter, model);
                //return CreatedAtRoute("GetAdminById", new { id = model.GetType().GetProperty("Id").GetValue(model, null) }, model);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode= HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}", Name = "[controller]/RemoveDataById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteData(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false));
                var Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>("u => u.Id ==" + id);
                var entity = await _db.GetAsync(filter: Filter);
                if (entity == null) { 
                return NotFound(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false));

                }
                await _db.RemoveAsync(entity);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "Record Deleted Successfully";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode= HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpDelete(Name = "[controller]/RemoveDataByFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteDataByFilter([FromQuery] string? filter =null)
        {
            try
            {
                List<TEntity> entity;
                Expression<Func<TEntity, bool>> Filter;
                if (filter!=null)
                {
                    Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>(filter);
                    entity = await _db.GetAllAsync(filter: Filter);
                }
                else
                {
                     entity = await _db.GetAllAsync();
                }

                if (entity == null)
                {
                    return NotFound(CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false));

                }
                await _db.RemoveByQueryAsync(entity);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "Record Deleted Successfully";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorsMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
