using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_ForSalesRep;
using System.Threading.Tasks;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ReportsStatic _report;
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _report = new ReportsStatic();
            _productService = productService;
        }

        [HttpGet("GetProductById/{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ProductViewModel>> GetById(int id)
        {
            var entity = await _productService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet("GetProductByTitle")]
        public async Task<ActionResult<ProductViewModel>> GetByTitle(string title)
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

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateProduct(int id, ProductViewModel productViewModel)
        {
            var entity = await _productService.UpdateAsync(id, productViewModel);
            return Ok(entity);
        }
        
        [HttpPost("AddProduct/{productModel}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddProduct(ProductViewModel productViewModel)
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
