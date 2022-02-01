using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomersAsync();
        Task<CustomerViewModel> GetById(int id);
        Task<OperationStatus> DeleteCustomerById(int id);
        Task<OperationStatus> UpdateAsync(int id, CustomerViewModel modelCustomerDTO);
        Task<OperationStatus> CreateCustomer(CustomerViewModel customerDTO);
    }
}
