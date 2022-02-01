using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        Task<ProductViewModel> GetById(int id);
        Task<OperationStatus> DeleteProductById(int id);
        Task<ProductViewModel> UpdateAsync(int id, ProductViewModel productDTO);
        Task<OperationStatus> AddProduct(ProductViewModel productDTO);
        Task<ProductViewModel> GetByTitle(string title);
    }
}
