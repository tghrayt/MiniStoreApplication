using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MiniStore.Helper
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly string _token;

        public BearerTokenHandler(string token)
        {
            _token = token;
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
