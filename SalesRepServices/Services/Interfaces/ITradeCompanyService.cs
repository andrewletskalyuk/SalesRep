using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepServices.Models;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ITradeCompanyService
    {
        Task<OperationStatus> CreateCompany(TradeCompanyModel tradeCompanyModel);
        Task<TradeCompanyModel> GetCompanyByTitle(string title);
        Task<OperationStatus> Update(TradeCompanyModel tradeCompanyModel);
        Task<OperationStatus> Delete(string title);
    }
}
