using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ITradeCompanyService
    {
        Task<OperationStatus> CreateCompany(TradeCompanyViewModel tradeCompanyVM);
        Task<TradeCompanyViewModel> GetCompanyByTitle(string title);
        Task<OperationStatus> Update(int id, TradeCompanyViewModel tradeCompanyVM);
        Task<OperationStatus> Delete(string title);
    }
}
