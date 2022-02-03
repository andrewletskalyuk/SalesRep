using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Threading.Tasks;

namespace SalesRepWebApi.Controllers
{
    [Route("api/product")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductById/{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            var entity = await _productService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet("GetProductByTitle/{title}")]
        public async Task<ActionResult<ProductModel>> GetByTitle(string title)
        {
            var entity = await _productService.GetByTitle(title);
            if (entity==null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        
        [HttpDelete("DeleteProductById/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductById(id);
            return Ok();
        }

        [HttpPut("UpdateProduct/{id}/{model}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel productViewModel)
        {
            var entity = await _productService.UpdateAsync(id, productViewModel);
            return Ok(entity);
        }
        
        [HttpPost("AddProduct/{model}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddProduct(ProductModel productViewModel)
        {
            if (productViewModel!=null)
            {
                await _productService.AddProduct(productViewModel);
                return Ok();
            }
            return BadRequest();
        }
    }
}
