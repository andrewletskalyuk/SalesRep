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
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _repository; //repository
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const string DEFAULT_PHONE = "+380671112233";

        public CustomerServiceTests()
        {
            _repository = new Mock<ICustomerRepository>();
            _mapper = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(CustomerProfile));
            }).CreateMapper();
            _customerService = new CustomerService(_mapper, _repository.Object);
        }

        [Fact]
        public void CreateCustomerTest_ReturnOperationStatus()
        {
            //Arrange
            var model = new CustomerModel()
            {
                Title = DEFAULT_STRING,
                IsActive = true,
                Address = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                AdditionalInfo = DEFAULT_STRING
            };

            _repository.Setup(x => x.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });

            //Act
            var result = _customerService.CreateCustomer(model).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetCustomersAsyncTest_ReturtList()
        {
            //Arrange
            IEnumerable<Customer> customers = new List<Customer>()
            {
                new Customer(){Title=DEFAULT_STRING, IsActive=true, Address= DEFAULT_STRING, Phone=DEFAULT_PHONE, AdditionalInfo=""},
                new Customer(){Title=DEFAULT_STRING, IsActive=true, Address= DEFAULT_STRING, Phone=DEFAULT_PHONE, AdditionalInfo=""},
                new Customer(){Title=DEFAULT_STRING, IsActive=false, Address= DEFAULT_STRING, Phone=DEFAULT_PHONE, AdditionalInfo=""},
            };

            _repository.Setup(x => x.GetCustomersAsync()).Returns(Task.FromResult(customers));

            //Act 
            var result = _customerService.GetCustomersAsync().GetAwaiter().GetResult();
            
            //Assert
            Assert.NotNull(result);
            Assert.Equal(customers.Count(), result.Count());
        }

        [Fact]
        public void DeleteCustomerById_ResultOperationStatus()
        {
            //Arrange
            int id = 0;
            _repository.Setup(x => x.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });
            _repository.Setup(s => s.DeleteCustomerById(id)).ReturnsAsync(new OperationStatus() { IsSuccess = true });

            //Act
            var result = _customerService.DeleteCustomerById(0).GetAwaiter().GetResult();

            //Assert
            var expectedResult = new OperationStatus() { IsSuccess = true };
            Assert.NotNull(result);
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
        }

        [Fact]
        public void GetCustomerById_ResultCustomerModel()
        {
            //Arrange 
            var customer = new Customer() { Title = DEFAULT_STRING, IsActive = true, Address = DEFAULT_STRING, Phone = DEFAULT_PHONE, AdditionalInfo = "" };
            int id = 0;
            _repository.Setup(x => x.CreateCustomer(It.IsAny<Customer>()));
            _repository.Setup(x => x.GetById(id)).Returns(Task.FromResult(customer));

            //Act
            var result = _customerService.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Result.CusomerID,customer.CusomerID);
        }

        [Fact]
        public void UpdateCustomer_ResultOperationStatus()
        {
            #region ===
            //Arrange
            //List<Customer> customers = new List<Customer>()
            //{
            //    new Customer(){Title=DEFAULT_STRING, IsActive=true, Address= DEFAULT_STRING, Phone=DEFAULT_PHONE, AdditionalInfo=""},
            //    new Customer(){Title=DEFAULT_STRING, IsActive=true, Address= DEFAULT_STRING, Phone=DEFAULT_PHONE, AdditionalInfo=""},
            //    new Customer(){Title=DEFAULT_STRING, IsActive=false, Address= DEFAULT_STRING, Phone=DEFAULT_PHONE, AdditionalInfo=""},
            //};
            //foreach (Customer customer in customers)
            //{
            //    _repository.Setup(x => x.CreateCustomer(customer)).ReturnsAsync(new OperationStatus() { IsSuccess = true });
            //} 
            #endregion
            //Arrange
            var customerz = new CustomerModel() { Title = DEFAULT_STRING, IsActive = true, Address = DEFAULT_STRING, Phone = DEFAULT_PHONE, AdditionalInfo = "" };
            _repository.Setup(mr => mr.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });
            
            //Act
            var updated = _customerService.UpdateAsync(customerz).GetAwaiter().GetResult();

            //Assert
            var expectedResult = new OperationStatus() { IsSuccess = true };
            Assert.NotNull(updated);
            Assert.Equal(updated.IsSuccess,expectedResult.IsSuccess);
        }
    }
}
