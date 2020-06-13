using Microsoft.Graph;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OnAirLight.AzureFunction.Graph
{
    public class AuthHandler : DelegatingHandler
    {
        private IAuthenticationProvider AuthenticationProvider { get; set; }

        public AuthHandler(IAuthenticationProvider authenticationProvider, HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
            AuthenticationProvider = authenticationProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await AuthenticationProvider.AuthenticateRequestAsync(request);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
