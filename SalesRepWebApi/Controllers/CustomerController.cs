using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesRepDAL;
using SalesRepServices.Helpers;
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
        private readonly ReportsStatic _report;
        private readonly ICustomerService _customerServices;
        public CustomerController(ICustomerService customerService)
        {
            _report = new ReportsStatic();
            _customerServices = customerService;
        }

        [HttpGet("GetAllCustomers")]
        [ProducesResponseType(200)]
        public async Task<Collection<CustomerDTO>> GetAllCustomers()
        {
            var customersDTO = await _customerServices.GetCustomersAsync();
            var collection = new Collection<CustomerDTO>
            {
                Value = customersDTO.ToArray()
            };
            return collection;
        }

        [HttpGet("GetCustomerByID/{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CustomerDTO>> GetById(int id)
        {
            var entity = await _customerServices.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpDelete("DeleteCustomerByID/{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _customerServices.DeleteCustomerById(id);
            return Ok();
        }

        [HttpPut("UpdateCustomer")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateAsync(int id, CustomerDTO updateCustomerModel)
        {
            var entity = await _customerServices.UpdateAsync(id, updateCustomerModel);
            return Ok(entity);
        }
        
        [HttpPost("CreateCustomer/{customerModel}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CreateCustomer(CustomerDTO customerDTO)
        {
            if (customerDTO != null)
            {
                await _customerServices.CreateCustomer(customerDTO);
                return Ok();
            }
            return BadRequest();
        }
    }
}
