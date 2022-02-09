using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
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
        public async Task<OperationStatus> DeleteProductById(int id)
        {
            if (id >= 0)
            {
                var res = await _productRepository.DeleteProductById(id);
                return res;
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            var res = new List<ProductModel>();
            foreach (var product in products)
            {
                res.Add(_mapper.Map<Product, ProductModel>(product));
            }
            return res;
        }
        public async Task<OperationStatus> UpdateAsync(ProductModel productModel)
        {
            if (productModel == null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "Product not found!" };
            }
            var map = _mapper.Map<ProductModel, Product>(productModel);
            return await _productRepository.UpdateAsync(map);
        }
        public async Task<ProductModel> GetByTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var entity = await _productRepository.GetByTitle(title);
                if (entity == null)
                {
                    return new ProductModel();
                }
                return _mapper.Map<Product, ProductModel>(entity);
            }
            return new ProductModel();
        }
        public async Task<OperationStatus> AddProduct(ProductModel productModel)
        {
            if (productModel != null)
            {
                var entity = _mapper.Map<ProductModel, Product>(productModel);
                return await _productRepository.AddProduct(entity);
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

    }
}
