using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Models;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_ForSalesRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ReportsStatic _report;
        private readonly ICustomerService _customerServices;
        public CustomerController(ICustomerService customerService, EFContext context)
        {
            _context = context;
            _report = new ReportsStatic();
            _customerServices = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IEnumerable<CustomerDTO>> GetAll()
        {
            var res = await _customerServices.GetAll();
            return Ok();
        }

        [HttpGet("{customerId}",Name ="GetCustomerByID")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CustomerDTO>> GetById(int id)
        {
            var entity = await _customerServices.GetById(id);
            if (entity==null)
            {
                return NotFound();
            }
            return Ok(entity);
            //try
            //{
            //    if (id > 0)
            //    {
            //        var res = _context.Customers.FirstOrDefault(x => x.CusomerID == id);
            //        if (res != null)
            //        {
            //            return Ok(res);
            //        }
            //    }
            //    throw new ArgumentException("Huston we have a problem!");
            //}
            //catch (Exception ex)
            //{
            //    _report.AnotherExeption(ex);
            //}
            //return BadRequest();
        }

        //[HttpGet("GetByTitle")]
        //public IActionResult GetByTitle(string title)
        //{
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(title))
        //        {
        //            var res = _context.Customers.FirstOrDefault(x => x.Title == title);
        //            if (res != null)
        //            {
        //                return Ok(res);
        //            }
        //        }
        //        throw new ArgumentException("Huston we have a problem!");
        //    }
        //    catch (Exception ex)
        //    {
        //        _report.AnotherExeption(ex);
        //    }
        //    return BadRequest();
        //}

    }
}
