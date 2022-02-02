using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ITradeOrderService
    {
        Task<OperationStatus> CreateOrder(TradeOrderViewModel tradeOrderViewModel);
        Task<List<TradeOrderViewModel>> GetOrdersOfCustomer(int CustomerId);
        Task<OperationStatus> Update(int id, TradeOrderViewModel tradeOrderViewModel);
        Task<OperationStatus> Delete(int id);
    }
}
