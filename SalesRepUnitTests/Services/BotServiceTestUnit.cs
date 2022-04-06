using Moq;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace SalesRepUnitTests.Services
{
    public class BotServiceTestUnit
    {
        private readonly Mock<ITradeOrderRepository> _repository;
        private readonly IBotService _botService;
        private readonly IHttpClientWrapper _httpClientWrapper;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const int DEFAULT_INT = 100;
        private const int DEFAULT_ID = 1;
        public BotServiceTestUnit()
        {
            _httpClientWrapper = new HttpClientWrapper();
            _repository = new Mock<ITradeOrderRepository>();
            _botService = new BotService(_httpClientWrapper);
        }
        [Fact]
        public void GetTradeOrdersByCustomerTest_Return200()
        {
            //Arrange
            var tradeOrder = new TradeOrder()
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = DEFAULT_STRING,
                SumOfOrder = DEFAULT_INT,
                AdditionalInfo = DEFAULT_STRING,
                CustomerID = DEFAULT_ID,
                Products = new List<Product>()
            };
            _repository.Setup(x => x.CreateOrder(It.IsAny<TradeOrder>()))
                        .ReturnsAsync(new OperationStatus() { IsSuccess = true });
            string host = "https://localhost:5001/";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(host + "api/TradeOrder/GetOrdersOfCustomer/" + 1 + "?customerId=" + 1),
                Method = HttpMethod.Get
            };

            // Act
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Assert
            using (var response = _httpClientWrapper.GetAsync(request.RequestUri.ToString()).Result)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
