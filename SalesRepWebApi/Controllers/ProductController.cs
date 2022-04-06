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

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProducts()
        {
            var res = await _productService.GetProductsAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("GetProductByTitle/{title}")]
        public async Task<ActionResult<ProductModel>> GetByTitle(string title)
        {
            var entity = await _productService.GetByTitle(title);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpDelete("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductById(id);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateProduct(ProductModel productModel)
        {
            var entity = await _productService.UpdateAsync(productModel);
            return Ok(entity);
        }

        [HttpPost("AddProduct/{productModel}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddProduct(ProductModel productModel)
        {
            if (productModel != null)
            {
                await _productService.AddProduct(productModel);
                return Ok();
            }
            return BadRequest();
        }
    }
}
