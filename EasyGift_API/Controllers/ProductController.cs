using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EasyGift_API.Controllers
{
    [Route("api/EasyGift/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CreateProductDTO>> GetProducts()
        {
            return Ok();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CreateProductDTO> GetProduct(int id)
        {
            if (id == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public ActionResult<CreateProductDTO> CreateProduct([FromBody]CreateProductDTO productDTO)
        {
            if(productDTO == null)
                return BadRequest(productDTO);
            return Ok();
        } 
    }
}
