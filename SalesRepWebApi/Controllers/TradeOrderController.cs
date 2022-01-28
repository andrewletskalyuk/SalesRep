using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Services_ForSalesRep;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TradeOrderController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ReportsStatic _report;
        public TradeOrderController(EFContext context)
        {
            _context = context;
            _report = new ReportsStatic();
        }

        [HttpGet("GetTradeOrderById")]
        public IActionResult GetTradeOrder(int id)
        {
            try
            {
                if (id>0)
                {
                    var res = _context.TradeOrders.FirstOrDefault(x => x.CustomerID == id);
                    if (res!=null)
                    {
                        return Ok(res);
                    }
                }
                throw new ArgumentException("Huston we have a problem!");
            }
            catch (Exception e)
            {
                _report.AnotherExeption(e);
            }
            return BadRequest();
        }

        //[HttpGet("GetOrdersByCustomer")]
        //public IActionResult Get(string title)
        //{
        //    try
        //    {
        //        var res = _context.TradeOrders.SelectMany()
        //    }
        //    catch (Exception e)
        //    {
        //        _report.AnotherExeption(e);
        //    }
        //    return BadRequest();
        //}

    }
}
