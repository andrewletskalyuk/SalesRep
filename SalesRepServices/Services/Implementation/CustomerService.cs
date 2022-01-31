using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfigurationProvider _mappingConguration;
        private readonly EFContext _context;
        public CustomerService(IConfigurationProvider mapConfiguration, EFContext context)
        {
            _mappingConguration = mapConfiguration;
            _context = context;
        }
        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            var query = _context.Customers
                            .ProjectTo<CustomerDTO>(_mappingConguration);
            return await query.ToArrayAsync();
        }

        public async Task<CustomerDTO> GetById(int id)
        {
            var entity = await _context.Customers
                             .SingleOrDefaultAsync(x => x.CusomerID == id);
            if (entity == null)
            {
                return null;
            }
            var mapper = _mappingConguration.CreateMapper();
            return mapper.Map<CustomerDTO>(entity);
        }
        public async Task DeleteCustomerById(int id)
        {
            var customerForDelete = await _context.Customers.FirstOrDefaultAsync(c => c.CusomerID == id);
            if (customerForDelete == null)
            {
                return;
            }
            _context.Customers.Remove(customerForDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomerDTO> UpdateAsync(int id, CustomerDTO updateCustomModel)
        {
            var customerForUpdate = await _context.Customers.FirstOrDefaultAsync(c => c.CusomerID == id);
            if (customerForUpdate == null)
            {
                return null;
            }
            var mapper = _mappingConguration.CreateMapper();
            var updatedModel = mapper.Map<Customer>(updateCustomModel);
            _context.Customers.Update(updatedModel);
            await _context.SaveChangesAsync();
            return updateCustomModel;
        }

        public async Task CreateCustomer(CustomerDTO customerDTO)
        {
            if (customerDTO != null)
            {
                var mapper = _mappingConguration.CreateMapper();
                var entity = mapper.Map<Customer>(customerDTO);
                _context.Customers.Add(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
