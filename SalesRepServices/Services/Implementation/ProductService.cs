using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(EFContext context, IMapper mapper, IProductRepository productRepository)
        {
            _context = context;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<OperationStatus> DeleteProductById(int id)
        {
            var productForDelete = await _context.Products.FirstOrDefaultAsync(x => x.ProductID == id);
            if (productForDelete == null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "Product not found!" };
            }
            _context.Products.Remove(productForDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<ProductModel> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductModel>(product);
        }
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            var res = new List<ProductModel>();
            foreach (var product in products)
            {
                res.Add(_mapper.Map<Product, ProductModel>(product));
            }
            return res;
        }
        public async Task<OperationStatus> UpdateAsync(ProductModel productModel)
        {
            var productForUpdate = await _context.Products.FirstOrDefaultAsync(x => x.ProductID == productModel.ProductID);
            if (productForUpdate == null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "Product not found!" };
            }
            var map = _mapper.Map<ProductModel, Product>(productModel, productForUpdate);
            _context.Products.Update(map);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<OperationStatus> AddProduct(ProductModel productModel)
        {
            if (productModel != null)
            {
                var entity = _mapper.Map<ProductModel, Product>(productModel);
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }
        public async Task<ProductModel> GetByTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var entity = await _context.Products
                            .SingleOrDefaultAsync(x => x.Title == title);
                if (entity == null)
                {
                    return new ProductModel();
                }
                return _mapper.Map<Product, ProductModel>(entity);
            }
            return new ProductModel();
        }

    }
}
