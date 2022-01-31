using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Services_ForSalesRep;
using System;
using System.Linq;

namespace SalesRepWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesRepController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly ReportsStatic _report;
        public SalesRepController(EFContext context)
        {
            _context = context;
            _report = new ReportsStatic();
        }
        [HttpGet("GetSalesRepByID")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var res = _context.SaleRep.FirstOrDefault(x => x.SaleRepID == id);
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
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            try
            {
                if (!String.IsNullOrEmpty(name))
                {
                    var res = _context.SaleRep
                                .Select(x => x)
                                .Where(x => x.FullName.Contains(name))
                                .ToList();
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
