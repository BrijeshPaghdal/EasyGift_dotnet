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
using System.Linq.Expressions;
using System.Net;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyGift_API.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAdmins([FromQuery] string[] columns, [FromQuery] string? filter = null)
        {
            try
            {
                IEnumerable<TEntity> admins;
                if (filter != null)
                {
                    var Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>(filter);
                    admins = await _db.GetAllAsync(filter: Filter);
                }
                else
                {
                    admins = await _db.GetAllAsync();
                }
                if (admins.Count() == 0)
                {
                    return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false);
                }
                if (columns.Length != 0)
                {
                    List<Dictionary<string, object>> fetchedAdmins = new List<Dictionary<string, object>>();
                    foreach (var admin in admins)
                    {
                        var response = CustomMethods<Admin>.fetchPerticularColumns(columns, admin);
                        if (response.ContainsKey("Error"))
                        {
                            return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]);
                        }
                        fetchedAdmins.Add(response);
                    }
                    _response.Result = fetchedAdmins;
                }
                else
                {
                    _response.Result = _mapper.Map<List<TEntityDto>>(admins);
                }
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDataById(int id, [FromQuery] string[] columns, [FromQuery] string? filter = null)
        {
            try
            {
                if (id == 0)
                {
                    return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false);
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
                    return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false);
                }
                TEntityDto model = _mapper.Map<TEntityDto>(entity);

                if (columns.Length != 0)
                {
                    var response = CustomMethods<TEntity>.fetchPerticularColumns(columns, model);
                    if (response.ContainsKey("Error"))
                    {
                        return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, Result: response["Error"]);
                    }
                    _response.Result = response;
                }
                else
                {
                    _response.Result = _mapper.Map<List<TEntityDto>>(model); ;
                }
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
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
                    return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, Result: createDTO);
                TEntity model = _mapper.Map<TEntity>(createDTO);
                await _db.CreateAsync(model);

                _response.Result = _mapper.Map<List<TEntityDto>>(model); ;
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return _response;
                //return CreatedAtRoute("GetDataById", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> UpdateData(int id, [FromBody] Dictionary<string, object> patchDTO)
        {
            try
            {
                if (patchDTO == null || id == 0)
                {
                    return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false);
                }
                var Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>("u => u.Id ==" + id);

                var entity = await _db.GetAsync(filter: Filter, tracked: false);
                if (entity == null)
                    return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false);

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
                        return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false, ErrorMessages: new List<string> { $"Invalid property name: {update.Key}" });
                    }
                }

                TUpdateDto updatedEntity = _mapper.Map<TUpdateDto>(entity);
                TEntity model = _mapper.Map<TEntity>(updatedEntity);

                await _db.UpdateAsync(model);
                if (!ModelState.IsValid)
                {
                    return CustomMethods<Admin>.ResponseBody(HttpStatusCode.BadRequest, false, Result: ModelState);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                //return CreatedAtRoute("GetDataById", Filter, model);
                //return CreatedAtRoute("GetAdminById", new { id = model.GetType().GetProperty("Id").GetValue(model, null) }, model);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteData(int id)
        {
            try
            {
                if (id == 0)
                    return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.BadRequest, false);
                var Filter = CustomMethods<TEntity>.ConvertToExpression<TEntity>("u => u.Id ==" + id);
                var entity = await _db.GetAsync(filter: Filter);
                if (entity == null) return CustomMethods<TEntity>.ResponseBody(HttpStatusCode.NotFound, false);
                await _db.RemoveAsync(entity);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "Record Deleted Successfully";

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
