using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [Authorize]
    public class SalesRepController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ISalesRepService _salesRepService;
        public SalesRepController(EFContext context, ISalesRepService salesRepService)
        {
            _context = context;
            _salesRepService = salesRepService;
        }

        [HttpPost("CreateSalesRep")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateSalesRep(SalesRepViewModel salesRepViewModel)
        {
            if (salesRepViewModel != null)
            {
                await _salesRepService.CreateRep(salesRepViewModel);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetRepByName/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SalesRepViewModel>> GetRepByName(string name)
        {
            var entity = await _salesRepService.GetByName(name);
            if (entity != null)
            {
                return Ok(entity);
            }
            return NotFound();
        }

        [HttpPut("UpdateSalesRep")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update(int id, SalesRepViewModel salesRepViewModel)
        {
            var entity = await _salesRepService.Update(id, salesRepViewModel);
            return Ok(entity);
        }

        [HttpDelete("DeleteSalesRepByName/{fullname}")]
        public async Task<IActionResult> Delete(string fullname)
        {
            await _salesRepService.DeleteByName(fullname);
            return Ok();
        }
    }
}

