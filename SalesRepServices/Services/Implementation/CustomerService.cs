using AutoMapper;
using AutoMapper.QueryableExtensions;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync()
        {
            var query = await _customerRepository.GetCustomersAsync();
            IList<CustomerModel> res = new List<CustomerModel>();
            if (query != null)
            {
                foreach (var customer in query)
                {
                    res.Add(_mapper.Map<Customer, CustomerModel>(customer));
                }
                return res;
            }
            return res;
        }

        public async Task<CustomerModel> GetById(int id)
        {
            if (id != 0)
            {
                var entity = await _customerRepository.GetById(id);
                if (entity != null)
                {
                    return _mapper.Map<Customer, CustomerModel>(entity);
                }
            }
            return new CustomerModel();
        }

        public async Task<OperationStatus> DeleteCustomerById(int id)
        {
            var operationStatus = await _customerRepository.DeleteCustomerById(id);
            if (operationStatus.IsSuccess)
            {
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }

        public async Task<OperationStatus> UpdateAsync(CustomerModel customerModel)
        {
            if (customerModel != null)
            {
                var temp = new Customer();
                var map = _mapper.Map<CustomerModel, Customer>(customerModel,temp);
                var res = await _customerRepository.UpdateAsync(map);
                return res;
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }

        public async Task<OperationStatus> CreateCustomer(CustomerModel customerModel)
        {
            if (customerModel != null)
            {
                var entity = _mapper.Map<CustomerModel, Customer>(customerModel);
                return await _customerRepository.CreateCustomer(entity);
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }
    }
}
