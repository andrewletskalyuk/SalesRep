using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<OperationStatus> CreateSupplier(SupplierModel supplierViewModel);
        Task<SupplierModel> GetByTitle(string title);
        Task<SupplierModel> GetSupplierWithProducts(string supplierTitle);
        Task<OperationStatus> Update(int id, SupplierModel supplierViewModel);
        Task<OperationStatus> Delete(string title);
    }
}
