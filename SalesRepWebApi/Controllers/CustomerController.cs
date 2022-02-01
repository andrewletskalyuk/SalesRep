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
        private readonly ICustomerService _customerServices;
        public CustomerController(ICustomerService customerService)
        {
            _customerServices = customerService;
        }

        [HttpGet("GetAllCustomers")]
        [ProducesResponseType(200)]
        public async Task<Collection<CustomerViewModel>> GetAllCustomers()
        {
            var customersViewModels = await _customerServices.GetCustomersAsync();
            var collection = new Collection<CustomerViewModel>
            {
                Value = customersViewModels.ToArray()
            };
            return collection;
        }

        [HttpGet("GetCustomerByID/{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CustomerViewModel>> GetById(int id)
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
        public async Task<IActionResult> UpdateAsync(int id, CustomerViewModel updateCustomerModel)
        {
            var entity = await _customerServices.UpdateAsync(id, updateCustomerModel);
            return Ok(entity);
        }
        
        [HttpPost("CreateCustomer/{customerModel}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CreateCustomer(CustomerViewModel customerDTO)
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
