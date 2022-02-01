using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfigurationProvider _mappingConguration;
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        public CustomerService(EFContext context, IMapper mapper, IConfigurationProvider mapConfiguration)
        {
            _mappingConguration = mapConfiguration;
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CustomerViewModel>> GetCustomersAsync()
        {
            var query = _context.Customers.ProjectTo<CustomerViewModel>(_mappingConguration);
            return await query.ToArrayAsync();
        }

        public async Task<CustomerViewModel> GetById(int id)
        {
            var entity = await _context.Customers
                             .SingleOrDefaultAsync(x => x.CusomerID == id);
            if (entity == null)
            {
                return null;
            }
            var mapper = _mappingConguration.CreateMapper();
            return mapper.Map<CustomerViewModel>(entity);
        }
        
        public async Task<OperationStatus> DeleteCustomerById(int id)
        {
            var customerForDelete = await _context.Customers.FirstOrDefaultAsync(c => c.CusomerID == id);
            if (customerForDelete == null)
            {
                return new OperationStatus() { IsSuccess = false, Message="204" };
            }
            _context.Customers.Remove(customerForDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }

        public async Task<OperationStatus> UpdateAsync(int id, CustomerViewModel updateCustomModel)
        {
            var customerForUpdate = await _context.Customers.FirstOrDefaultAsync(c => c.CusomerID == id);
            if (customerForUpdate == null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            var map = _mapper.Map<CustomerViewModel,Customer>(updateCustomModel,customerForUpdate);
            _context.Customers.Update(map);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200"};
        }

        public async Task<OperationStatus> CreateCustomer(CustomerViewModel customerDTO)
        {
            if (customerDTO != null)
            {
                var mapper = _mappingConguration.CreateMapper();
                var entity = mapper.Map<Customer>(customerDTO);
                _context.Customers.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true, Message = "200" };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }
    }
}
