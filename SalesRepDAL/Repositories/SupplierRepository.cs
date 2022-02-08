using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly EFContext _context;
        public SupplierRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<OperationStatus> CreateSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<OperationStatus> Delete(string title)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Title == title);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Supplier didn't found" };
        }
        public async Task<Supplier> GetByTitle(string title)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Title == title);
            if (supplier != null)
            {
                return supplier;
            }
            return new Supplier();
        }
        public async Task<List<Product>> GetProductsOfSupplier(string supplierTitle)
        {
            var supplier = _context.Suppliers.FirstOrDefault(z => z.Title == supplierTitle);
            return _context.Products.Where(x => x.SupplierID == supplier.SupplierID).ToList();
        }
        public async Task<List<Supplier>> SearchByTitle(string text)
        {
            return _context.Suppliers.Where(x => x.Title.Contains(text)).ToList();
        }
        public async Task<OperationStatus> Update(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new OperationStatus() { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
