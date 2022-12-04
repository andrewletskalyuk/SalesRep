using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SalesRepUnitTests.Services
{
    public class BotServiceTest
    {
        private readonly Mock<ITradeOrderRepository> _repository;
        private readonly IBotService _botService;
        private readonly IHttpClientWrapper _httpClientWrapper;
        private const string DefaultString = "DEFAULT_STRING";
        private const int DefaultInt = 100;
        private const int DefaultId = 1;
        public BotServiceTest()
        {
            _httpClientWrapper = new HttpClientWrapper();
            _repository = new Mock<ITradeOrderRepository>();
            _botService = new BotService(_httpClientWrapper);
        }
        [Fact]
        public async Task GetTradeOrdersByCustomerIDAsyncTest_ReturnListAsync()
        {
            //Arrange
            var tradeOrder = new TradeOrder()
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = DefaultString,
                SumOfOrder = DefaultInt,
                AdditionalInfo = DefaultString,
                CustomerID = DefaultId,
                Products = new List<Product>()
            };
            var saveTradeOrder = _repository.Setup(x => x.CreateOrder(It.IsAny<TradeOrder>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });
            var jsonObject = JsonConvert.SerializeObject(tradeOrder);
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonObject)
                });
            string host = "https://localhost:5001/";

            // Act
            var result = await _botService.GetListTradeOrders(DefaultId, host);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count>0);

        }
    }
}
