using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ITradeCompanyService
    {
        Task<OperationStatus> CreateCompany(TradeCompanyModel tradeCompanyVM);
        Task<TradeCompanyModel> GetCompanyByTitle(string title);
        Task<OperationStatus> Update(int id, TradeCompanyModel tradeCompanyVM);
        Task<OperationStatus> Delete(string title);
    }
}
