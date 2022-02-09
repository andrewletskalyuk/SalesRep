using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SalesRep_IntegrationTests.Controllers
{
    public class IntegrationTestBase
    {
        protected readonly HttpClient _httpClient;
        public IntegrationTestBase()
        {
            var factory = new WebApplicationFactory<Startup>()
        }
    }
}
