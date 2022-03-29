using SalesRepServices.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
