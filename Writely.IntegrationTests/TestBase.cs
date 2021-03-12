using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Writely.IntegrationTests
{
    public class TestBase
    {
        protected readonly WebAppFactory<Startup> _factory;
        protected readonly HttpClient _client;

        public TestBase(WebAppFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}