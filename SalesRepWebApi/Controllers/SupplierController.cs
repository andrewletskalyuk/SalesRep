using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
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
    public class SupplierController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ReportsStatic _report;
        public SupplierController(EFContext context)
        {
            _context = context;
            _report = new ReportsStatic();
        }

        [HttpGet("GetSupplierByID")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var res = _context.Suppliers.FirstOrDefault(x => x.SupplierID == id);
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

        //[HttpGet("GetByTitle")]
        //public IActionResult GetByTitle(string title)
        //{
            //try
            //{
            //    if (!String.IsNullOrEmpty(title))
            //    {
            //        var res = _context.Suppliers.FirstOrDefault(x => x. == title);
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
        //}
    }
}
