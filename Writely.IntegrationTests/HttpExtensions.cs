using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Writely.IntegrationTests
{
    public static class HttpExtensions
    {
        public static StringContent ToJson<T>(this T request)
        {
            var serializedRequest = JsonSerializer.Serialize(request);
            return new StringContent(serializedRequest, Encoding.UTF8, "application/json");
        }
    }
}