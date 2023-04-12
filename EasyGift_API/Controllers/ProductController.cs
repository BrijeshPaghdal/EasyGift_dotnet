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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyGift_API.Controllers
{
    [Route("api/EasyGift/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _dbProduct;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository dbProduct,IMapper mapper)
        {
            _dbProduct = dbProduct;
            _mapper= mapper;
        }
        
        //Http Requests

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetProducts([FromQuery] string[] columns, [FromQuery] string? filter= null)
        {
            IEnumerable<Product> products;
            if (filter!=null)
            {
                var Filter = CustomMethods<Product>.ConvertToExpression<Product>(filter);
                 products = await _dbProduct.GetAllAsync(filter: Filter);
            }
            else
            {
                 products = await _dbProduct.GetAllAsync();
            }
            if (products.Count() == 0)
                return NotFound();
            if (columns.Length != 0)
            {
                List<Dictionary<string, object>> fetchedProducts = new List<Dictionary<string, object>>();
                foreach(var product in products)
                {
                    var response = CustomMethods<Product>.fetchPerticularColumns(columns, product);
                    if (response.ContainsKey("Error"))
                    {
                        return BadRequest(response["Error"]);
                    }
                   fetchedProducts.Add(response);
                }
                return Ok(fetchedProducts);
            }
            return Ok(_mapper.Map<List<ProductDTO>>(products));
        }

     
        [HttpGet("{id:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string,object>>> GetProduct(int id,[FromQuery] string[] columns, [FromQuery] string? filter = null)
        {
            if (id == 0)
                return BadRequest();
            Product product=new Product();
            if (filter != null)
            {
                var Filter = CustomMethods<Product>.ConvertToExpression<Product>(filter+ "&& u => u.ProductId =="+id);
                product = await _dbProduct.GetAsync(filter: Filter);
            }
            else
            {
                product = await _dbProduct.GetAsync(u => u.ProductId == id);
            }

            if (product == null)
                return NotFound();
            ProductDTO model = _mapper.Map<ProductDTO>(product);

            if (columns.Length != 0) {
                var response = CustomMethods<Product>.fetchPerticularColumns(columns, model);
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
        public async Task<ActionResult<CreateAddressDTO>> CreateProduct([FromBody] CreateAddressDTO createProductDTO)
        {
            if (createProductDTO == null)
                return BadRequest(createProductDTO);
            Product model = _mapper.Map<Product>(createProductDTO);
            await _dbProduct.CreateAsync(model);
            return CreatedAtRoute("GetProductById", new { id = model.ProductId }, model);
        }



        [HttpPatch("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Dictionary<string, object> patchDTO)
        {
            if(patchDTO == null || id==0)
                return BadRequest();
            var product =await _dbProduct.GetAsync(u => u.ProductId == id,tracked:false);
            if(product == null)
                return NotFound();

            foreach (var update in patchDTO)
            {
                var property = product.GetType().GetProperty(update.Key);

                if (property != null)
                {
                    var convertedValue = Convert.ChangeType(update.Value, property.PropertyType);
                    property.SetValue(product, convertedValue);
                }
                else
                {
                    return BadRequest($"Invalid property name: {update.Key}");
                }
            }

            UpdateProductDTO updatedProduct = _mapper.Map<UpdateProductDTO>(product);
            Product model = _mapper.Map<Product>(updatedProduct);

            await _dbProduct.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetProductById", new { id = model.ProductId }, model);

        }



        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(id == 0)
                return BadRequest();
            var product = await _dbProduct.GetAsync(u=>u.ProductId == id);
            if(product == null) return NotFound();
            await _dbProduct.RemoveAsync(product);
            Dictionary<string,object> responseSuccess= new Dictionary<string, object>();
            responseSuccess.Add("message","Record Deleted Successfully");
            return Ok(responseSuccess);
        }
    }
}
