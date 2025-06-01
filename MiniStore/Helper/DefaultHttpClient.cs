using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Http;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MiniStore.Helper
{
    public class DefaultHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public DefaultHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, Stream contentStream, CancellationToken cancellationToken)
        {
            var content = new StreamContent(contentStream);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return _httpClient.PostAsync(requestUri, content, cancellationToken);
        }

        public void Configure(IConfiguration configuration)
        {
            // Pas besoin de config dynamique ici
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
