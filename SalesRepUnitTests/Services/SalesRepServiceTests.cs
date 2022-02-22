using AutoMapper;
using Moq;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.MappingProfiles;
using SalesRepServices.Models;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SalesRepUnitTests.Services
{
    public class SalesRepServiceTests
    {
        private readonly Mock<ISalesRepRepository> _repository;
        private readonly Mock<ICustomerRepository> _repositoryCustomer;
        private readonly Mock<ITradeOrderRepository> _repositoryTradeOrder;
        private readonly IMapper _mapper;
        private readonly IMapper _mapperCustomer;
        private readonly IMapper _mapperTradeOrder;
        private readonly ISalesRepService _salesRepService;
        private readonly ICustomerService _customerService;
        private readonly ITradeOrderService _tradeOrderService;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const string DEFAULT_PHONE = "38 067 111 22 33";
        private const int DEFAULT_INT = 100;
        private const string DEFAULT_EMAIL = "andrewletskalyuk@gmail.com";

        public SalesRepServiceTests()
        {
            _repository = new Mock<ISalesRepRepository>();
            _mapper = new MapperConfiguration(config=> {
                config.AddMaps(typeof(SalesRepProfile));
            }).CreateMapper();
            _salesRepService = new SalesRepService(_mapper,_repository.Object);


            _repositoryCustomer = new Mock<ICustomerRepository>();
            _mapperCustomer = new MapperConfiguration(config=> { 
                config.AddMaps(typeof(CustomerProfile));
            }).CreateMapper();
            _customerService = new CustomerService(_mapperCustomer, _repositoryCustomer.Object);

            _repositoryTradeOrder = new Mock<ITradeOrderRepository>();
            _mapperTradeOrder = new MapperConfiguration(config => {
                config.AddMaps(typeof(TradeOrderProfile));
            }).CreateMapper();
            _tradeOrderService = new TradeOrderService(_mapperTradeOrder, _repositoryTradeOrder.Object);
        }

        [Fact]
        public void CreateSalesRep_ResultOperationStatus()
        {
            //Arrange
            var salesRepModel = new SalesRepModel()
            {
                FullName = DEFAULT_STRING,
                HomeAddress = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Salary = DEFAULT_INT,
                IsActive = true,
                Itinerary = DEFAULT_STRING,
                Email = DEFAULT_EMAIL,
                TradeCompanyID = 1
            };
            _repository.Setup(x => x.CreateRep(It.IsAny<SaleRep>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });

            //Act
            var result = _salesRepService.CreateRep(salesRepModel).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetRepByName_ResultSalesRepModel()
        {
            //Arrange
            var salesRepModel = new SalesRepModel() {
                FullName = DEFAULT_STRING,
                HomeAddress = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Salary = DEFAULT_INT,
                IsActive = true,
                Itinerary = DEFAULT_STRING,
                Email = DEFAULT_EMAIL};
            var salesRep = _mapper.Map<SaleRep>(salesRepModel);
            _repository.Setup(x => x.GetByName(DEFAULT_STRING)).Returns(Task.FromResult(salesRep));

            //Act
            var result = _salesRepService.GetByName(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.FullName, salesRep.FullName);
        }

        [Fact]
        public void UpdateSalesRep_ReturnSalesRepStatus()
        {
            //Arrange
            var salesRepModel = new SalesRepModel() {
                FullName = DEFAULT_STRING,
                HomeAddress = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Salary = DEFAULT_INT,
                IsActive = true,
                Itinerary = DEFAULT_STRING,
                Email = DEFAULT_EMAIL};
            _repository.Setup(g => g.Update(It.IsAny<SaleRep>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });

            //Act
            var updated = _salesRepService.Update(salesRepModel).GetAwaiter().GetResult();

            //Assert
            var expectedSalesRep = new OperationStatus() { IsSuccess = true };
            Assert.NotNull(updated);
            Assert.Equal(updated.IsSuccess, expectedSalesRep.IsSuccess);
        }

        [Fact]
        public void DeleteSalesRepByName_ResultOperationStatus()
        {
            //Arrange
            _repository.Setup(x => x.CreateRep(It.IsAny<SaleRep>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });
            _repository.Setup(s=>s.DeleteByName(DEFAULT_STRING)).ReturnsAsync(new OperationStatus() { IsSuccess=true});

            //Act
            var result = _salesRepService.DeleteByName(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            var expectedResult = new OperationStatus() { IsSuccess = true };
            Assert.NotNull(result);
            Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
        }

        [Fact]
        public void GetCustomersOfSalesRep_ResultListCustomers()
        {
            //Arrange
            var salesRep = new SaleRep()
            {
                FullName = DEFAULT_STRING,
                Email = DEFAULT_EMAIL,
                HomeAddress = DEFAULT_STRING,
                IsActive = true,
                Itinerary = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Salary = DEFAULT_INT,
                SaleRepID = 100,
                TradeCompanyID = 1
            };
            _repository.Setup(x => x.CreateRep(It.IsAny<SaleRep>()))
                .ReturnsAsync(new OperationStatus() { IsSuccess = true });
            var resultSalesRep = _salesRepService
                .CreateRep(_mapper.Map<SalesRepModel>(salesRep))
                .GetAwaiter().GetResult();

            var model = new Customer()
            {
                Title = DEFAULT_STRING,
                IsActive = true,
                Address = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                AdditionalInfo = DEFAULT_STRING,
                CusomerID = 100
            };
            var model1 = new Customer()
            {
                Title = DEFAULT_STRING + "1",
                IsActive = true,
                Address = DEFAULT_STRING + "1",
                Phone = DEFAULT_PHONE,
                AdditionalInfo = DEFAULT_STRING,
                CusomerID = 99
            };

            _repositoryCustomer.Setup(x => x.CreateCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(new OperationStatus() { IsSuccess = true });

            var resultCC = _customerService
                .CreateCustomer(_mapperCustomer.Map<CustomerModel>(model))
                .GetAwaiter().GetResult();
            var result1CC = _customerService
                .CreateCustomer(_mapperCustomer.Map<CustomerModel>(model1))
                .GetAwaiter().GetResult();

            var to1 = new TradeOrder()
            {
                CreatedDate = DateTime.Now,
                DeliveryAddress = DEFAULT_STRING,
                SumOfOrder = DEFAULT_INT,
                AdditionalInfo = DEFAULT_STRING,
                SalesRepID = 100,
                CustomerID = 99
            };
            var to2 = new TradeOrder()
            {
                CreatedDate = DateTime.Now,
                DeliveryAddress = DEFAULT_STRING,
                SumOfOrder = DEFAULT_INT,
                AdditionalInfo = DEFAULT_STRING,
                SalesRepID = 100,
                CustomerID = 99
            };
            _repositoryTradeOrder.Setup(x => x.CreateOrder(It.IsAny<TradeOrder>()))
                .ReturnsAsync(new OperationStatus() { IsSuccess = true });
            var resultTO = _tradeOrderService
                .CreateOrder(_mapperTradeOrder.Map<TradeOrderModel>(to1))
                .GetAwaiter().GetResult();
            var resultTO1 = _tradeOrderService
                .CreateOrder(_mapperTradeOrder.Map<TradeOrderModel>(to2))
                .GetAwaiter().GetResult();

            //Act
            var result = _salesRepService.GetCustomersOfSalesRep(DEFAULT_STRING)
                .GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 0);
        }
    }
}
