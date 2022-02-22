using Microsoft.EntityFrameworkCore;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories
{
    public class SalesRepRepository : ISalesRepRepository
    {
        private readonly EFContext _context;
        public SalesRepRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<OperationStatus> CreateRep(SaleRep saleRep)
        {
            _context.SaleRep.Add(saleRep);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<OperationStatus> DeleteByName(string name)
        {
            _context.SaleRep.Remove(_context.SaleRep.FirstOrDefault(x => x.FullName == name));
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<SaleRep> GetByName(string name)
        {
            return _context.SaleRep.FirstOrDefault(x => x.FullName == name);
        }

        public async Task<IList<CustomerOfSalesRepModel>> GetCustomersOfSalesRep(string nameSalesRep)
        {
            var result = from tradeO in _context.TradeOrders
                         join cust in _context.Customers on tradeO.CustomerID equals cust.CusomerID
                         join salesRep in _context.SaleRep on tradeO.SalesRepID equals salesRep.SaleRepID
                         where salesRep.FullName == nameSalesRep
                         select new CustomerOfSalesRepModel { Title = cust.Title, Address = cust.Address };
            var models = new List<CustomerOfSalesRepModel>();
            foreach (var item in result)
            {
                models.Add(item);
            }
            return models;
        }

        public async Task<OperationStatus> Update(SaleRep saleRep)
        {
            var company = _context.Trades.FirstOrDefault(x => x.TradeCompanyID == saleRep.TradeCompanyID);
            if (company != null)
            {
                _context.SaleRep.Update(saleRep);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Trade company didn't found!" };
        }
    }
}
