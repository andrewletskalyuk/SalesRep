using SalesRepServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetById(int id);
        Task DeleteCustomerById(int id);
        Task<CustomerDTO> UpdateAsync(int id, CustomerDTO updateTodoItemModel);
    }
}
