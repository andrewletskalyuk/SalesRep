using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_ForSalesRep;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SupplierController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ISupplierService _supplierService;
        public SupplierController(EFContext context, ISupplierService salesRepService)
        {
            _context = context;
            _supplierService = salesRepService;
        }

        [HttpPost("CreateSupplier")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateSupplier(SupplierViewModel supplierViewModel)
        {
            if (supplierViewModel != null)
            {
                await _supplierService.CreateSupplier(supplierViewModel);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetSupplier/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SupplierViewModel>> GetSupplier(string title)
        {
            var entity = await _supplierService.GetByTitle(title);
            if (entity != null)
            {
                return Ok(entity);
            }
            return NotFound();
        }

        [HttpGet("GetSuppWithProducts")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<SupplierViewModel>> GetSupWithProducts(string supplierTitle)
        {
            var array = await _supplierService.GetSupplierWithProducts(supplierTitle);
            if (array!=null)
            {
                return Ok(array);
            }
            return BadRequest();
        }
    
        [HttpPut("UpdateSupplier")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierViewModel supplierViewModel)
        {
            var entity = await _supplierService.Update(id, supplierViewModel);
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
    }
}
