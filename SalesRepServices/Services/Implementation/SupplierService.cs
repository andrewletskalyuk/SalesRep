using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        public SupplierService(EFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OperationStatus> CreateSupplier(SupplierViewModel supplierViewModel)
        {
            if (supplierViewModel!=null)
            {
                var entity = _mapper.Map<SupplierViewModel, Supplier>(supplierViewModel);
                _context.Suppliers.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true, Message = "200" };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> Delete(string title)
        {
            var supplierForDelete = await _context.Suppliers.FirstOrDefaultAsync(x => x.Title == title);
            if (supplierForDelete==null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            _context.Suppliers.Remove(supplierForDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }

        public async Task<SupplierViewModel> GetByTitle(string title)
        {
            var entity = await _context.Suppliers
                        .SingleOrDefaultAsync(x => x.Title == title);
            if (entity==null)
            {
                return new SupplierViewModel();
            }
            return _mapper.Map<Supplier, SupplierViewModel>(entity);
        }

        public async Task<SupplierViewModel> GetSupplierWithProducts(string supplierTitle)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Title == supplierTitle);
            var listProducts = _context.Products
                            .Where(x => x.SupplierID == supplier.SupplierID)
                            .SelectMany(z=> _context.Products.Where(y=>y.SupplierID==z.SupplierID));
            var mapProduct = _mapper.Map<List<ProductViewModel>>(listProducts);
            var mapSupplier = _mapper.Map<SupplierViewModel>(supplier);
            mapSupplier.Products.AddRange(mapProduct);
            if (mapSupplier == null)
            {
                return new SupplierViewModel();
            }
            return mapSupplier;
        }

        public async Task<OperationStatus> Update(int id, SupplierViewModel supplierViewModel)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x=>x.SupplierID == id);
            if (supplier== null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            var map = _mapper.Map<SupplierViewModel,Supplier>(supplierViewModel,supplier);
            _context.Suppliers.Update(map);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }
    }
}
