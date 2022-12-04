using SalesRepDAL.Entities;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class BotService : IBotService, IDisposable
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        public BotService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public void Dispose()
        {
            _httpClientWrapper?.Dispose();
        }

        public async Task<List<TradeOrder>> GetListTradeOrders(int id, string host)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                host + "api/TradeOrder/GetOrdersOfCustomer/" + id + "?customerId=" + id);
            var Uri = request.RequestUri.AbsoluteUri;
            var responseQ = await _httpClientWrapper.GetAsync(Uri);
            if (!responseQ.IsSuccessStatusCode)
            {
                return new List<TradeOrder>();
            }
            var responseString = await responseQ.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var tradeOrders = JsonSerializer.Deserialize<List<TradeOrder>>(responseString, options);
            return tradeOrders;
        }
    }
}
