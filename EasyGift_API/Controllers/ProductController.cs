using AutoMapper;
using Azure;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyGift_API.Controllers
{
    [Route("api/EasyGift/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper= mapper;
        }


        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            IEnumerable<Product> products = await _db.Product.ToListAsync();
            return Ok(_mapper.Map<List<ProductDTO>>(products));
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            if (id == 0)
                return BadRequest();
            var Products = await _db.Product.FirstOrDefaultAsync(u => u.ProductId == id);
            if (Products == null)
                return NotFound();
            return Ok(_mapper.Map<ProductDTO>(Products));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateProductDTO>> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null)
                return BadRequest(createProductDTO);
            Product model = _mapper.Map<Product>(createProductDTO);
            await _db.Product.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetProductById", new { id = model.ProductId }, model);
        }

        [HttpPatch("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Dictionary<string, object> patchDTO){
            if(patchDTO == null || id==0)
                return BadRequest();
            var product =await _db.Product.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == id);
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

            _db.Product.Update(model);
            await _db.SaveChangesAsync();
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
            var product = _db.Product.FirstOrDefault(u=>u.ProductId == id);
            if(product == null) return NotFound();
            _db.Product.Remove(product);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
