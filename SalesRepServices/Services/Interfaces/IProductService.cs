using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductModel> GetById(int id);
        Task<ProductModel> GetByTitle(string title);
        Task<OperationStatus> DeleteProductById(int id);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<OperationStatus> UpdateAsync(ProductModel productDTO);
        Task<OperationStatus> AddProduct(ProductModel productDTO);
    }
}
