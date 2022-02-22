using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.MappingProfiles;
using SalesRepServices.Models;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_ForSalesRep;
using SalesRepServices.Services_Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SalesRep_IntegrationTests.Controllers
{
    public class TradeOrderEndPointTests
    {
        private readonly ITradeOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITradeOrderService _tradeOrderService;
        private readonly ILogsReport _logsReport;
        private readonly EFContext _context;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const string DEFAULT_PHONE = "+380671112233";
        private const string DEFAULT_EMAIL = "defaultemail@gmail.com";
        private const int DEFAULT_INT = 100;
        public TradeOrderEndPointTests()
        {
            var builder = new DbContextOptionsBuilder<EFContext>();
            builder.UseSqlServer($"Data Source=localhost;Database=SalesRepDB;Integrated Security=True; ApplicationIntent=ReadWrite;");
            _context = new EFContext(builder.Options);
            //_context.Database.Migrate();
            _repository = new TradeOrderRepository(_context);
            _mapper = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(TradeOrderProfile));
            }).CreateMapper();
            _logsReport = new LogsReport();
            _tradeOrderService = new TradeOrderService(_mapper, _logsReport, _repository);
        }

        [Fact]
        public void CreateOrderTest_ReturnOperationStatus()
        {
            //Arrange
            var orderModel = new TradeOrder()
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Today.AddDays(1),
                DeliveryAddress = DEFAULT_STRING,
                SumOfOrder = DEFAULT_INT,
                AdditionalInfo = DEFAULT_STRING,
                CustomerID = 1
            };
            var orderModelUpdate = new TradeOrderModel()
            {
                CreatedDate = DateTime.Now,
                DeliveryAddress = DEFAULT_STRING,
                SumOfOrder = DEFAULT_INT,
                AdditionalInfo = DEFAULT_STRING,
                CustomerID = 1
            };

            var resultAddModelToDB = _repository.CreateOrder(orderModel).GetAwaiter().GetResult();

            //Act
            var result = _tradeOrderService
                .CreateOrder(orderModelUpdate)
                .GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsSuccess, resultAddModelToDB.IsSuccess);
        }

        [Fact]
        public void DeleteOrderTest_ReturnOperationStatus()
        {
            //Arrange
            var orderModel = new TradeOrder()
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Today.AddDays(1),
                DeliveryAddress = DEFAULT_STRING,
                SumOfOrder = DEFAULT_INT,
                AdditionalInfo = DEFAULT_STRING,
                CustomerID = 1
            };
            var resultAddModelToDB = _repository.CreateOrder(orderModel).GetAwaiter().GetResult();

            //Act
            var result = _tradeOrderService.Delete(orderModel.TradeOrderID).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetOrdersOfCustomerTest_ReturnOperationStatus()
        {
            //Arrange
            var orderModels = new List<TradeOrder>()
            {
                new TradeOrder()
                {
                    CreatedDate = DateTime.Now,
                    DeliveryDate = DateTime.Today.AddDays(1),
                    DeliveryAddress = DEFAULT_STRING,
                    SumOfOrder = DEFAULT_INT,
                    AdditionalInfo = DEFAULT_STRING,
                    CustomerID = 1
                }, new TradeOrder()
                {
                    CreatedDate = DateTime.Now,
                    DeliveryDate = DateTime.Today.AddDays(1),
                    DeliveryAddress = DEFAULT_STRING,
                    SumOfOrder = DEFAULT_INT,
                    AdditionalInfo = DEFAULT_STRING,
                    CustomerID = 1
                }
            };
            foreach (var item in orderModels)
            {
                _repository.CreateOrder(item).GetAwaiter().GetResult();
            }

            //Act
            var result = _tradeOrderService.GetOrdersOfCustomer(1).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void UpdateTradeOrderTest_ResultOperationStatus()
        {
            //Arrange
            var orderModel = new TradeOrder()
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Today.AddDays(1),
                DeliveryAddress = DEFAULT_STRING,
                SumOfOrder = DEFAULT_INT,
                AdditionalInfo = DEFAULT_STRING,
                CustomerID = 1
            };
            _repository.CreateOrder(orderModel).GetAwaiter().GetResult();

            var orderForUpdate = new TradeOrderModel()
            {
                AdditionalInfo = DEFAULT_STRING,
                DeliveryAddress = DEFAULT_STRING,
                CustomerID = 1,
                SumOfOrder = DEFAULT_INT,
                CreatedDate = DateTime.Now
            };

            //Act
            var result = _tradeOrderService.Update(orderForUpdate).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }
    }
}
