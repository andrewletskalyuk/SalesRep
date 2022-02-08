using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories.Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetById(int id);
        Task<OperationStatus> DeleteCustomerById(int id);
        Task<OperationStatus> UpdateAsync(Customer customer);
        Task<OperationStatus> CreateCustomer(Customer customer);
    }
}
