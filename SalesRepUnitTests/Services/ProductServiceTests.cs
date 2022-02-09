using AutoMapper;
using Moq;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.MappingProfiles;
using SalesRepServices.Models;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SalesRepUnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const int DEFAULT_INT = 100;
        public ProductServiceTests()
        {
            _repository = new Mock<IProductRepository>();
            _mapper = new MapperConfiguration(config =>
                {
                    config.AddMaps(typeof(ProductProfile));
                }).CreateMapper();
            _productService = new ProductService(_mapper, _repository.Object);
        }

        [Fact]
        public void CreateProduct_ReturnOperationStatus()
        {
            //Arrange
            var product = new ProductModel() { Title = DEFAULT_STRING, Price = DEFAULT_INT, Description = DEFAULT_STRING, QuantityInWarehouse = DEFAULT_INT, TotalSum = DEFAULT_INT };
            _repository.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });

            //Act
            var result = _productService.AddProduct(product).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetProductById_ReturnOperationStatus()
        {
            //Arrange 
            var product = new Product() { Title = DEFAULT_STRING, Price = DEFAULT_INT, Description = DEFAULT_STRING, QuantityInWarehouse = DEFAULT_INT, TotalSum = DEFAULT_INT };
            int id = 0;
            _repository.Setup(z => z.AddProduct(product)).Returns(Task.FromResult(new OperationStatus() { IsSuccess = true }));
            _repository.Setup(x => x.GetById(id)).Returns(Task.FromResult(product));

            //Act
            var result = _productService.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Result.ProductID, product.ProductID);
        }

        [Fact]
        public void GetAllProducts_ReturnList()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                new Product(){Title=DEFAULT_STRING, QuantityInWarehouse=DEFAULT_INT, Price=DEFAULT_INT, TotalSum=DEFAULT_INT, Description=DEFAULT_STRING},
                new Product(){Title=DEFAULT_STRING, QuantityInWarehouse=DEFAULT_INT, Price=DEFAULT_INT, TotalSum=DEFAULT_INT, Description=DEFAULT_STRING},
                new Product(){Title=DEFAULT_STRING, QuantityInWarehouse=DEFAULT_INT, Price=DEFAULT_INT, TotalSum=DEFAULT_INT, Description=DEFAULT_STRING}
            };

            _repository.Setup(x => x.GetProductsAsync()).Returns(Task.FromResult(products));

            //Act
            var result = _productService.GetProductsAsync().GetAwaiter().GetResult();
            
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Count(),products.Count());
        }

        [Fact]
        public void GetProductByTitle_ReturnProductModel()
        {
            //Arrange
            var product = new Product() { Title = DEFAULT_STRING, Price = DEFAULT_INT, Description = DEFAULT_STRING, QuantityInWarehouse = DEFAULT_INT, TotalSum = DEFAULT_INT };
            _repository.Setup(x => x.GetByTitle(DEFAULT_STRING)).Returns(Task.FromResult(product));

            //Act
            var result = _productService.GetByTitle(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Title, product.Title);
        }

        [Fact]
        public void DeleteProductById_ReturnOperationStatus()
        {
            //Arrange
            int id = 0;
            _repository.Setup(z => z.AddProduct(It.IsAny<Product>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });
            _repository.Setup(x => x.DeleteProductById(id)).ReturnsAsync(new OperationStatus() { IsSuccess = true });

            //Act
            var result = _productService.DeleteProductById(id).GetAwaiter().GetResult();

            //Assert
            var expectedResult = new OperationStatus() { IsSuccess = true };
            Assert.NotNull(result);
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
        }
    
        [Fact]
        public void UpdateProduct_ReturnOperationStatus()
        {
            //Arrange
            var product = new ProductModel() { Title = DEFAULT_STRING, Price = DEFAULT_INT, Description = DEFAULT_STRING, QuantityInWarehouse = DEFAULT_INT, TotalSum = DEFAULT_INT };
            _repository.Setup(rt => rt.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(new OperationStatus() { IsSuccess=true});

            //Act
            var updated = _productService.UpdateAsync(product).GetAwaiter().GetResult();

            //Assert
            var expectedResult = new OperationStatus() { IsSuccess = true };
            Assert.NotNull(updated);
            Assert.Equal(updated.IsSuccess,expectedResult.IsSuccess);
        }
    }
}
