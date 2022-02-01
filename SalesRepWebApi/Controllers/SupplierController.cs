using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
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
    [ApiController]
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

        [HttpGet("GetSupplierByName/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SupplierViewModel>> GetSupByName(string title)
        {
            var entity = await _supplierService.GetByName(title);
            if (entity != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("GetSuppWithProducts")]
        public async Task<ActionResult<SupplierViewModel>> GetSupWithProducts(string supplierTitle)
        {
            var array = await _supplierService.GetSupplierWithProducts(supplierTitle);
            if (array!=null)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
