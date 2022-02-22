using SalesRepDAL.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductModel> GetById(int id);
        Task<OperationStatus> DeleteProductById(int id);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<OperationStatus> UpdateAsync(ProductModel productModel);
        Task<ProductModel> GetByTitle(string title);
        Task<OperationStatus> AddProduct(ProductModel productModel);
    }
}
