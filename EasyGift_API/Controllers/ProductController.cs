using Azure;
using EasyGift_API.Data;
using EasyGift_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyGift_API.Controllers
{
    [Route("api/EasyGift/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            return Ok(await _db.Product.ToListAsync());
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
            return Ok(Products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateProductDTO>> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            if (productDTO == null)
                return BadRequest(productDTO);
            //await _db.Product.AddAsync(productDTO);
            //await _db.SaveChanges();
            //return CreatedAtRoute("GetProductById", new { id = productDTO.ProductId }, productDTO);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateProduct(int id, JsonPatchDocument<UpdateProductDTO> patchDTO){
            if(patchDTO == null || id==0)
                return BadRequest();
            var product =await _db.Product.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == id);

            //patchDTO.ApplyTo(productDTO, ModelState);


            //await _db.Product.Update(productDTO);
            //await _db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetProductById", new { id = product.ProductId }, product);

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
