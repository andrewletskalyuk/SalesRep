using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories
{
    public class TradeOrderRepository: ITradeOrderRepository
    {
        private readonly EFContext _context;
        public TradeOrderRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<OperationStatus> CreateOrder(TradeOrder tradeOrder)
        {
            _context.TradeOrders.Add(tradeOrder);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<OperationStatus> Delete(int id)
        {
            var res = _context.TradeOrders.FirstOrDefault(x => x.TradeOrderID == id);
            if (res != null)
            {
                _context.TradeOrders.Remove(res);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Order wasn't found!" };
        }
        public async Task<List<TradeOrder>> GetOrdersOfCustomer(int customerId)
        {
            return _context.TradeOrders.Where(x => x.CustomerID == customerId).ToList();
        }
        public async Task<OperationStatus> Update(TradeOrder tradeOrder)
        {
            _context.TradeOrders.Update(tradeOrder);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
    }
}
