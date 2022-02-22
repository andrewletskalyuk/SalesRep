using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories.Contracts
{
    public interface ISalesRepRepository
    {
        Task<OperationStatus> CreateRep(SaleRep saleRep);
        Task<OperationStatus> DeleteByName(string name);
        Task<SaleRep> GetByName(string name);
        Task<OperationStatus> Update(SaleRep saleRep);
        Task<IList<CustomerOfSalesRepModel>> GetCustomersOfSalesRep(string nameSalesRep);
    }
}
