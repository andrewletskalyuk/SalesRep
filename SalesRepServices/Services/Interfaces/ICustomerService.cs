using SalesRepServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAll();
        Task<CustomerDTO> GetById(int id);
        Task<CustomerResponseModel> UpdateAsync(int id, CustomerDTO updateTodoItemModel);
        Task<BaseModel> DeleteCustomerById(int id);
    }
}
