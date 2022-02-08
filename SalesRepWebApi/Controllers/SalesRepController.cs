using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Threading.Tasks;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
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
        [ProducesResponseType(201)] //created
        public async Task<IActionResult> CreateSalesRep(SalesRepModel salesRepModel)
        {
            if (salesRepModel != null)
            {
                await _salesRepService.CreateRep(salesRepModel);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetRepByName/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)] //No Content
        public async Task<ActionResult<SalesRepModel>> GetRepByName(string name)
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
        public async Task<IActionResult> Update(SalesRepModel salesRepModel)
        {
            var entity = await _salesRepService.Update(salesRepModel);
            return Ok(entity);
        }

        [HttpDelete("DeleteSalesRepByName/{fullname}")]
        [ProducesResponseType(200)] 
        public async Task<IActionResult> Delete(string fullname)
        {
            await _salesRepService.DeleteByName(fullname);
            return Ok();
        }
    }
}

