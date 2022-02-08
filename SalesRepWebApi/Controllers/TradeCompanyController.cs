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
    public class TradeCompanyController : ControllerBase
    {
        private readonly ITradeCompanyService _tcService;
        public TradeCompanyController(ITradeCompanyService tcService)
        {
            _tcService = tcService;
        }

        [HttpPost("CreateCompany")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateCompany(TradeCompanyModel tradeCompanyModel)
        {
            if (tradeCompanyModel!=null)
            {
                await _tcService.CreateCompany(tradeCompanyModel);
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
        public async Task<IActionResult> GetByTitle(string title)
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
        public async Task<IActionResult> Update(TradeCompanyModel tcModel)
        {
            var entity = await _tcService.Update(tcModel);
            return Ok(entity);
        }
    }
}
