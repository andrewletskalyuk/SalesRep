using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories.Contracts
{
    public interface ITradeCompanyRepository
    {
        Task<OperationStatus> CreateCompany(TradeCompany tradeCompany);
        Task<OperationStatus> Delete(string title);
        Task<TradeCompany> GetCompanyByTitle(string title);
        Task<OperationStatus> Update(TradeCompany tradeCompany);
    }
}
