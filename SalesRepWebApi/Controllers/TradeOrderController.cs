using Microsoft.AspNetCore.Mvc;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_Interfaces;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TradeOrderController : ControllerBase
    {
        private readonly ITradeOrderService _tradeOrderService;
        private readonly IReportsInLog _logs;
        public TradeOrderController(IReportsInLog logs, ITradeOrderService tradeOrderService)
        {
            _tradeOrderService = tradeOrderService;
            _logs = logs;
        }

        [HttpPost("CreateOrder")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateOrder(TradeOrderViewModel orderViewModel)
        {
            if (orderViewModel != null)
            {
                await _tradeOrderService.CreateOrder(orderViewModel);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteOrder/{id}")]
        [ProducesResponseType(202)] //accepted
        [ProducesResponseType(204)] //no content
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _tradeOrderService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logs.AnotherExeption(ex);
            }
            return BadRequest();
        }

        [HttpGet("GetOrdersOfCustomer")]
        public async Task<IActionResult> GetOrders(int customerId)
        {
            try
            {
                var res = await _tradeOrderService.GetOrdersOfCustomer(customerId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logs.AnotherExeption(ex);
            }
            return BadRequest();
        }

        [HttpPut("EditOrder")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, TradeOrderViewModel tradeOrderViewModel)
        {
            try
            {
                var entity = await _tradeOrderService.Update(id, tradeOrderViewModel);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logs.AnotherExeption(ex);
            }
            return BadRequest();
        }
    }
}
