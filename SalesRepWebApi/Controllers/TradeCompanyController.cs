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
    [Authorize]
    public class TradeCompanyController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ITradeCompanyService _tcService;
        public TradeCompanyController(EFContext context, ITradeCompanyService tcService)
        {
            _context = context;
            _tcService = tcService;
        }

        [HttpPost("CreateCompany")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateCompany(TradeCompanyViewModel tcViewModel)
        {
            if (tcViewModel!=null)
            {
                await _tcService.CreateCompany(tcViewModel);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteCompany/{title}")]
        [ProducesResponseType(202)] //accepted
        [ProducesResponseType(204)] //no content
        public async Task<IActionResult> DeleteCompany(string title)
        {
            await _tcService.Delete(title);
            return Ok();
        }
        
        [HttpGet("GetCompanyByTitle/{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)] //No Content
        public async Task<ActionResult<TradeCompanyViewModel>> GetByTitle(string title)
        {
            var res = await _tcService.GetCompanyByTitle(title);
            if (res!=null)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [HttpPut("UpdateCompany")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, TradeCompanyViewModel tcViewModel)
        {
            var entity = await _tcService.Update(id, tcViewModel);
            return Ok(entity);
        }
    }
}
