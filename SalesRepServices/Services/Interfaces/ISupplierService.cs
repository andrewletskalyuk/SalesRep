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
        Task<OperationStatus> CreateSupplier(SupplierViewModel supplierViewModel);
        Task<SupplierViewModel> GetByName(string title);
        Task<SupplierViewModel> GetSupplierWithProducts(string supplierTitle);
        Task<OperationStatus> Update(int id, SupplierViewModel supplierViewModel);
        Task<OperationStatus> Delete(string title);
    }
}
