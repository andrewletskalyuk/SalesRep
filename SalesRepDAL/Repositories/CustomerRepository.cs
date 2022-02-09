using Microsoft.EntityFrameworkCore;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EFContext _context;
        public CustomerRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var res = await _context.Customers.ToListAsync();
            return res;
        }
        public async Task<Customer> GetById(int id)
        {
            var res = await _context.Customers.FirstOrDefaultAsync(x => x.CusomerID == id);
            return res;
        }
        public async Task<OperationStatus> DeleteCustomerById(int id)
        {
            var customerForDelete = await _context.Customers.FirstOrDefaultAsync(x => x.CusomerID == id);
            if (customerForDelete != null)
            {
                _context.Customers.Remove(customerForDelete);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }
        public async Task<OperationStatus> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<OperationStatus> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
    }
}
