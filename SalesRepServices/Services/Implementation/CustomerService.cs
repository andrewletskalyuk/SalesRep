using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IEnumerable<CustomerDTO>> GetAll()
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

        public async Task<CustomerResponseModel> UpdateAsync(int id, CustomerDTO updateTodoItemModel)
        {
            var customerForUpdate = await _context.Customers.FirstOrDefaultAsync(c => c.CusomerID == id);
            var mapper = _mappingConguration.CreateMapper();
            return mapper.Map<CustomerResponseModel>(customerForUpdate);
        }

        public async Task<BaseModel> DeleteCustomerById(int id)
        {
            var customerForDelete = await _context.Customers.FirstOrDefaultAsync(c=>c.CusomerID == id);
            var mapper = _mappingConguration.CreateMapper();
            return mapper.Map<BaseModel>(customerForDelete);
        }
    }
}
