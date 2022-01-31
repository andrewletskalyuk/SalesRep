using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> GetById(int id);
        Task<OperationStatus> DeleteProductById(int id);
        Task<ProductDTO> UpdateAsync(int id, ProductDTO productDTO);
        Task<OperationStatus> AddProduct(ProductDTO productDTO);
        Task<ProductDTO> GetByTitle(string title);
    }
}
