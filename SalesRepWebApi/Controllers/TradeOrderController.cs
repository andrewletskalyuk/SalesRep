using Microsoft.AspNetCore.Mvc;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_Interfaces;
using System;
using System.Threading.Tasks;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TradeOrderController : ControllerBase
    {
        private readonly ITradeOrderService _tradeOrderService;
        private readonly ILogsReport _logs;
        public TradeOrderController(ILogsReport logs, ITradeOrderService tradeOrderService)
        {
            _tradeOrderService = tradeOrderService;
            _logs = logs;
        }

        [HttpPost("CreateOrder/{model}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateOrder(TradeOrderModel orderModel)
        {
            if (orderModel != null)
            {
                await _tradeOrderService.CreateOrder(orderModel);
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

        [HttpGet("GetOrdersOfCustomer/{id}")]
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

        [HttpPut("EditOrder/{id}/{model}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, TradeOrderModel tradeOrderModel)
        {
            try
            {
                var entity = await _tradeOrderService.Update(id, tradeOrderModel);
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
