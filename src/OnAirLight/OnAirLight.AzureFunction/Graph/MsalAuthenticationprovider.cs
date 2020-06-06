using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OnAirLight.AzureFunction.Graph
{
    public class MsalAuthenticationProvider : IAuthenticationProvider
    {
        private IConfidentialClientApplication ClientApplication { get; }
        private string[] Scopes { get; }

        public MsalAuthenticationProvider(IConfidentialClientApplication clientApplication, string[] scopes)
        {
            ClientApplication = clientApplication;
            Scopes = scopes;
        }

        /// <summary>
        /// Update HttpRequestMessage with credentials
        /// </summary>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var token = await GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        /// <summary>
        /// Acquire Token 
        /// </summary>
        public async Task<string> GetTokenAsync()
        {
            AuthenticationResult authResult = await ClientApplication.AcquireTokenForClient(Scopes)
                                .ExecuteAsync();
            return authResult.AccessToken;
        }
    }
}
