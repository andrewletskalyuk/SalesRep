using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories.Contracts
{
    public interface ISupplierRepository
    {
        Task<OperationStatus> CreateSupplier(Supplier supplier);
        Task<OperationStatus> Delete(string title);
        Task<Supplier> GetByTitle(string title);
        Task<List<Product>> GetProductsOfSupplier(string supplierTitle);
        Task<List<Supplier>> SearchByTitle(string text);
        Task<OperationStatus> Update(Supplier supplier);
    }
}
