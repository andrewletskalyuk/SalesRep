using Microsoft.AspNetCore.Mvc;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Threading.Tasks;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService salesRepService)
        {
            _supplierService = salesRepService;
        }

        [HttpPost("CreateSupplier/{model}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateSupplier(SupplierModel supplierModel)
        {
            if (supplierModel != null)
            {
                await _supplierService.CreateSupplier(supplierModel);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetSupplier/{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SupplierModel>> GetSupplier(string title)
        {
            var entity = await _supplierService.GetByTitle(title);
            if (entity != null)
            {
                return Ok(entity);
            }
            return NotFound();
        }

        [HttpGet("GetProductsOfSupplier/{title}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<SupplierModel>> GetProductsOfSupplier(string supplierTitle)
        {
            var array = await _supplierService.GetProductsOfSupplier(supplierTitle);
            if (array!=null)
            {
                return Ok(array);
            }
            return BadRequest();
        }
    
        [HttpPut("UpdateSupplier/{model}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateSupplier(SupplierModel supplierModel)
        {
            var entity = await _supplierService.Update(supplierModel);
            return Ok(entity);
        }

        [HttpDelete("DeleteSupplier/{title}")]
        [ProducesResponseType(202)] //accepted
        [ProducesResponseType(204)] //no content
        public async Task<IActionResult> Delete(string title)
        {
            await _supplierService.Delete(title);
            return Ok();
        }

        [HttpGet("SearchByTitle/{title}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Search(string title)
        {
            var res =  await _supplierService.SearchByTitle(title);
            return Ok(res);
        }
    }
}
