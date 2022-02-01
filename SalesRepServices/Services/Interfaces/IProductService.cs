using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> GetById(int id);
        Task<ProductViewModel> GetByTitle(string title);
        Task<OperationStatus> DeleteProductById(int id);
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        Task<OperationStatus> UpdateAsync(int id, ProductViewModel productDTO);
        Task<OperationStatus> AddProduct(ProductViewModel productDTO);
    }
}
