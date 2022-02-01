using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class ProductService : IProductService
    {
        private readonly IConfigurationProvider _mappingConguration;
        private readonly EFContext _context;
        
        public ProductService(IConfigurationProvider mapConfiguration, EFContext context)
        {
            _mappingConguration = mapConfiguration;
            _context = context;
        }
        public async Task<OperationStatus> DeleteProductById(int id)
        {
            var productForDelete = await _context.Products.FirstOrDefaultAsync(x => x.ProductID == id);
            if (productForDelete==null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            _context.Products.Remove(productForDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }
        public async Task<ProductViewModel> GetById(int id)
        {
            var entity = await _context.Products
                              .SingleOrDefaultAsync(x => x.ProductID == id);
            if (entity == null)
            {
                return null;
            }
            var mapper = _mappingConguration.CreateMapper();
            return mapper.Map<ProductViewModel>(entity);
        }
        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            var query = _context.Products
                        .ProjectTo<ProductViewModel>(_mappingConguration);
            return await query.ToArrayAsync();
        }
        public async Task<ProductViewModel> UpdateAsync(int id, ProductViewModel productDTO)
        {
            var productForUpdate = await _context.Products.FirstOrDefaultAsync(x => x.ProductID == id);
            if (productForUpdate==null)
            {
                return null;
            }
            productForUpdate.Title = productDTO.Title;
            productForUpdate.QuantityInWarehouse = productDTO.QuantityInWarehouse;
            productForUpdate.Price = productDTO.Price;
            productForUpdate.Description = productDTO.Description;
            productForUpdate.TotalSum = productDTO.TotalSum;
            _context.Products.Update(productForUpdate);
            await _context.SaveChangesAsync();
            return productDTO;
        }
        public async Task<OperationStatus> AddProduct(ProductViewModel productDTO)
        {
            if (productDTO!=null)
            {
                var mapper = _mappingConguration.CreateMapper();
                var entity = mapper.Map<Product>(productDTO);
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { Message = "200", IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false };
        }

        public async Task<ProductViewModel> GetByTitle(string title)
        {
            var entity = await _context.Products
                        .SingleOrDefaultAsync(x => x.Title == title);
            if (entity==null)
            {
                return null;
            }
            var mapper = _mappingConguration.CreateMapper();
            return mapper.Map<ProductViewModel>(entity);
        }
    }
}
