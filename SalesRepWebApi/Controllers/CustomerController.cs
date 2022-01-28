using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Services_ForSalesRep;
using System;
using System.Linq;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ReportsStatic _report;
        public CustomerController(EFContext context)
        {
            _context = context;
            _report = new ReportsStatic();
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAll()
        {
            try
            {
                var res = _context.Customers.OrderBy(x => x.IsActive).ToList();
                if (res != null)
                {
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                _report.AnotherExeption(ex);
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }

        [HttpGet("GetCustomerByID")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var res = _context.Customers.FirstOrDefault(x => x.CusomerID == id);
                    if (res != null)
                    {
                        return Ok(res);
                    }
                }
                throw new ArgumentException("Huston we have a problem!");
            }
            catch (Exception ex)
            {
                _report.AnotherExeption(ex);
            }
            return BadRequest();
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            try
            {
                if (!String.IsNullOrEmpty(title))
                {
                    var res = _context.Customers.FirstOrDefault(x => x.Title == title);
                    if (res != null)
                    {
                        return Ok(res);
                    }
                }
                throw new ArgumentException("Huston we have a problem!");
            }
            catch (Exception ex)
            {
                _report.AnotherExeption(ex);
            }
            return BadRequest();
        }

        #region for another time
        //// POST api/<CustomerController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CustomerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CustomerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //} 
        #endregion
    }
}
