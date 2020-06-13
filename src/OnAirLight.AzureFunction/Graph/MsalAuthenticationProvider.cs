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
            AuthenticationResult authResult = null;
            try
            {
                authResult = await ClientApplication.AcquireTokenForClient(Scopes)
                                   .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // The application doesn't have sufficient permissions.
                // - Did you declare enough app permissions during app creation?
                // - Did the tenant admin grant permissions to the application?
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be in the form "https://resourceurl/.default"
                // Mitigation: Change the scope to be as expected.
            }


            return authResult.AccessToken;
        }
    }
}
