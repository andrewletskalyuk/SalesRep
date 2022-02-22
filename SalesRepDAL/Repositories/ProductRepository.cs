using Microsoft.EntityFrameworkCore;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EFContext _context;
        public ProductRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<Product> GetById(int id)
        {
            var entity = await _context.Products
                              .SingleOrDefaultAsync(x => x.ProductID == id);

            return entity;
        }
        public async Task<OperationStatus> DeleteProductById(int id)
        {
            var productDelete = _context.Products.FirstOrDefault(x => x.ProductID == id);
            _context.Products.Remove(productDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToArrayAsync();
            return products;
        }
        public async Task<OperationStatus> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<Product> GetByTitle(string title)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Title == title);
        }
        public async Task<OperationStatus> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
    }
}
