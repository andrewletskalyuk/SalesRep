using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetById(int id);
        Task<OperationStatus> DeleteCustomerById(int id);
        Task<CustomerDTO> UpdateAsync(int id, CustomerDTO modelCustomerDTO);
        Task<OperationStatus> CreateCustomer(CustomerDTO customerDTO);
    }
}
