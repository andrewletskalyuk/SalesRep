using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories.Contracts
{
    public interface ITradeOrderRepository
    {
        Task<OperationStatus> CreateOrder(TradeOrder tradeOrder);
        Task<OperationStatus> Delete(int id);
        Task<List<TradeOrder>> GetOrdersOfCustomer(int customerId);
        Task<OperationStatus> Update(TradeOrder tradeOrder);
    }
}
