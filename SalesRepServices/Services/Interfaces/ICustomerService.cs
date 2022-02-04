using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetCustomersAsync();
        Task<CustomerModel> GetById(int id);
        Task<OperationStatus> DeleteCustomerById(int id);
        Task<OperationStatus> UpdateAsync(CustomerModel modelCustomerDTO);
        Task<OperationStatus> CreateCustomer(CustomerModel customerDTO);
    }
}
