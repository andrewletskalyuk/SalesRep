using SalesRepDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface IBotService
    {
        Task<List<TradeOrder>> GetListTradeOrders(int id, string host);
    }
}
