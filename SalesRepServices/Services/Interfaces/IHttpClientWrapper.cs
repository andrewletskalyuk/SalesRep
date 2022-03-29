using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface IHttpClientWrapper : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
