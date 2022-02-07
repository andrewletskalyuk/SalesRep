using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<OperationStatus> CreateSupplier(SupplierModel supplierModel);
        Task<SupplierModel> GetByTitle(string title);
        Task<IList<ProductModel>> GetProductsOfSupplier(string supplierTitle);
        Task<OperationStatus> Update(SupplierModel supplierModel);
        Task<OperationStatus> Delete(string title);
        Task<List<SupplierModel>> SearchByTitle(string text);
    }
}
