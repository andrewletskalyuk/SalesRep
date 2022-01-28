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
    public class ProductController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ReportsStatic _report;
        public ProductController(EFContext context)
        {
            _context = context;
            _report = new ReportsStatic();
        }

        [HttpGet("GetProductById")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id>0)
                {
                    var res = _context.Products.FirstOrDefault(x => x.ProductID == id);
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

        [HttpGet("GetProductByTitle")]
        public IActionResult GetByTitle(string title)
        {
            try
            {
                if (!String.IsNullOrEmpty(title))
                {
                    var res = _context.Products.FirstOrDefault(x => x.Title == title);
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
    }
}
