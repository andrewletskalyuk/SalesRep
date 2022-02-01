using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_ForSalesRep;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("GetProductById")]
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
        [HttpDelete("DeleteProductById")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductById(id);
            return NoContent();
        }

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateProduct(int id, ProductViewModel productDTO)
        {
            var entity = await _productService.UpdateAsync(id, productDTO);
            return Ok(entity);
        }
        
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductViewModel productDTO)
        {
            if (productDTO!=null)
            {
                await _productService.AddProduct(productDTO);
                return Ok();
            }
            return BadRequest();//test1
        }
    }
}
