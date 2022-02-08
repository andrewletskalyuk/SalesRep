using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetById(int id);
        Task<OperationStatus> DeleteProductById(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<OperationStatus> UpdateAsync(Product product);
        Task<Product> GetByTitle(string title);
        Task<OperationStatus> AddProduct(Product product);
    }
}
