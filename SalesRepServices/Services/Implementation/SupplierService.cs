using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        private readonly ILogsReport _logsReport;
        public SupplierService(EFContext context, IMapper mapper, ILogsReport logsReport)
        {
            _context = context;
            _mapper = mapper;
            _logsReport = logsReport;
        }
        public async Task<OperationStatus> CreateSupplier(SupplierModel supplierModel)
        {
            if (supplierModel != null)
            {
                var entity = _mapper.Map<SupplierModel, Supplier>(supplierModel);
                _context.Suppliers.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> Delete(string title)
        {
            var supplierForDelete = await _context.Suppliers.FirstOrDefaultAsync(x => x.Title == title);
            if (supplierForDelete == null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            _context.Suppliers.Remove(supplierForDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }

        public async Task<SupplierModel> GetByTitle(string title)
        {
            var entity = await _context.Suppliers
                        .SingleOrDefaultAsync(x => x.Title == title);
            if (entity == null)
            {
                return new SupplierModel();
            }
            return _mapper.Map<Supplier, SupplierModel>(entity);
        }

        //переробити де List
        public async Task<IList<ProductModel>> GetProductsOfSupplier(string supplierTitle)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Title == supplierTitle);
            var res = new List<ProductModel>();
            if (supplier != null)
            {
                //IQueryable<Product> products = _context.Products.Where(x => x.SupplierID == supplier.SupplierID);
                var products = _context.Products.Where(x => x.SupplierID == supplier.SupplierID);
                try
                {
                    foreach (var Product in products) //ToList() - приведення до 
                    {
                        res.Add(_mapper.Map<Product, ProductModel>(Product));
                    }
                    return res;
                }
                catch (Exception ex)
                {
                    _logsReport.AnotherExeption(ex);
                }
            }
            return res;
        }

        public async Task<List<SupplierModel>> SearchByTitle(string text)
        {
            IQueryable<Supplier> suppliers = _context.Suppliers
                                                .Where(x => x.Title.Contains(text) 
                                                      && !String.IsNullOrEmpty(text));
            var res = new List<SupplierModel>();
            if (suppliers!=null)
            {
                foreach (var Supplier in suppliers)
                {
                    res.Add(_mapper.Map<Supplier, SupplierModel>(Supplier));
                }
                return res;
            }
            return res;
        }

        public async Task<OperationStatus> Update(SupplierModel supplierModel)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierID == supplierModel.SupplierID);
            if (supplier == null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            var map = _mapper.Map<SupplierModel, Supplier>(supplierModel, supplier);
            _context.Suppliers.Update(map);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
    }
}
