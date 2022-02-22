using SalesRepDAL.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ITradeOrderService
    {
        Task<OperationStatus> CreateOrder(TradeOrderModel tradeOrderModel);
        Task<List<TradeOrderModel>> GetOrdersOfCustomer(int CustomerId);
        Task<OperationStatus> Update(TradeOrderModel tradeOrderModel);
        Task<OperationStatus> Delete(int id);
    }
}
