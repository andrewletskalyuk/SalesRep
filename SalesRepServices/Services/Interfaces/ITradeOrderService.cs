using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ITradeOrderService
    {
        Task<OperationStatus> CreateOrder(TradeOrderModel tradeOrderViewModel);
        Task<List<TradeOrderModel>> GetOrdersOfCustomer(int CustomerId);
        Task<OperationStatus> Update(int id, TradeOrderModel tradeOrderViewModel);
        Task<OperationStatus> Delete(int id);
    }
}
