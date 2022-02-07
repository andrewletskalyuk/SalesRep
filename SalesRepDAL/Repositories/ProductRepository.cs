using Microsoft.EntityFrameworkCore;
using SalesRepDAL.Entities;
using SalesRepDAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
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
    }
}
