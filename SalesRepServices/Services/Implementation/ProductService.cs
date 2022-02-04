using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IConfigurationProvider _mappingConguration;
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        
        public ProductService(IConfigurationProvider mapConfiguration, 
                EFContext context, IMapper mapper)
        {
            _mappingConguration = mapConfiguration;
            _context = context;
            _mapper = mapper;
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
        public async Task<ProductModel> GetById(int id)
        {
            var entity = await _context.Products
                              .SingleOrDefaultAsync(x => x.ProductID == id);
            if (entity == null)
            {
                return null;
            }
            var mapper = _mappingConguration.CreateMapper();
            return mapper.Map<ProductModel>(entity);
        }
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var query = _context.Products
                        .ProjectTo<ProductModel>(_mappingConguration);
            return await query.ToArrayAsync();
        }
        public async Task<OperationStatus> UpdateAsync(ProductModel productModel)
        {
            var productForUpdate = await _context.Products.FirstOrDefaultAsync(x => x.ProductID == productModel.ProductID);
            if (productForUpdate==null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            var map = _mapper.Map<ProductModel, Product>(productModel, productForUpdate);
            _context.Products.Update(map);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess=true, Message = "200"};
        }
        public async Task<OperationStatus> AddProduct(ProductModel productModel)
        {
            if (productModel!=null)
            {
                var mapper = _mappingConguration.CreateMapper();
                var entity = mapper.Map<ProductModel,Product>(productModel);
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { Message = "200", IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }
        public async Task<ProductModel> GetByTitle(string title)
        {
            var entity = await _context.Products
                        .SingleOrDefaultAsync(x => x.Title == title);
            if (entity==null)
            {
                return new ProductModel();
            }
            return _mapper.Map<Product,ProductModel>(entity);
        }

    }
}
