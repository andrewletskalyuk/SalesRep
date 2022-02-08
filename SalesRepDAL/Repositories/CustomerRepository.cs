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
            _context.Customers
                    .Remove(await _context.Customers
                                          .FirstOrDefaultAsync(x => x.CusomerID == id));
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
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
