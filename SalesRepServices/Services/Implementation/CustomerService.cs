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
        //private readonly IConfigurationProvider _mappingConguration;
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        public CustomerService(IConfigurationProvider mapConfiguration, EFContext context, IMapper mapper)
        {
            //_mappingConguration = mapConfiguration;
            _context = context;
            _mapper = mapper;
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

        public async Task<CustomerDTO> UpdateAsync(int id, CustomerDTO updateCustomModel)
        {
            var customerForUpdate = await _context.Customers.FirstOrDefaultAsync(c => c.CusomerID == id);
            if (customerForUpdate == null)
            {
                return null; //need add exception
            }
            //customerForUpdate.AdditionalInfo = updateCustomModel.AdditionalInfo;
            //customerForUpdate.Address = updateCustomModel.Address;
            //customerForUpdate.IsActive = updateCustomModel.IsActive;
            //customerForUpdate.Phone = updateCustomModel.Phone;
            //customerForUpdate.Title = updateCustomModel.Title;
            //var mapper = _mappingConguration.CreateMapper();
            //var entity = mapper.Map<Customer>(updateCustomModel);
            ////_context.Customers.Update(customerForUpdate);
            //_context.Customers.Update(customerForUpdate);
            var map = _mapper.Map<CustomerDTO, Customer>(updateCustomModel);
            _context.Customers.Update(map);
            await _context.SaveChangesAsync();
            return updateCustomModel;
        }

        public async Task<OperationStatus> CreateCustomer(CustomerDTO customerDTO)
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
