using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnAirLight.AzureFunction.Graph
{
    /// <remarks>
    /// See https://docs.microsoft.com/en-us/graph/api/resources/presence?view=graph-rest-beta for valid presence values
    /// </remarks>
    public class GraphService : IGraphService
    {
        public GraphService(GraphServiceConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            var authenticationProvider = CreateAuthorizationProvider();
            Client = new GraphServiceClient(authenticationProvider);
        }

        private GraphServiceClient Client { get; set; }
        private GraphServiceConfig Config { get; }

        private IAuthenticationProvider CreateAuthorizationProvider()
        {

            //this specific scope means that application will default to what is defined in the application registration rather than using dynamic scopes
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var builder = ConfidentialClientApplicationBuilder.Create(Config.ClientId)
                                              .WithAuthority(AzureCloudInstance.AzurePublic, Config.TenantId)
                                              //.WithAuthority(Config.Authority)
                                              //.WithRedirectUri(Config.RedirectUri)
                                              .WithClientSecret(Config.ClientSecret)
                                              .WithLogging(MyLoggingMethod, LogLevel.Info,
                                                    enablePiiLogging: true,
                                                    enableDefaultPlatformLogging: true)
                                              .Build();

            return new MsalAuthenticationProvider(builder, scopes); ;
        }

        void MyLoggingMethod(LogLevel level, string message, bool containsPii)
        {
            Console.WriteLine($"MSAL Level:{level} PII:{containsPii} {Environment.NewLine} Message: {message}");
        }

        public async Task<(string Availability, string Activity)> GetPresence(string userId)
        {

            //List<QueryOption> options = new List<QueryOption>
            //{
            //    new QueryOption("$top", "1")
            //};

            //var graphResult = await Client.Users.Request(options).GetAsync();

            var response = await Client.Communications.GetPresencesByUserId(new[] { userId })
                .Request()
                .PostAsync();


            if (response is null || response.Count == 0)
            {
                throw new InvalidOperationException($"Presence info not found for user ID '{userId}'");
            }

            if (response.Count > 1)
            {
                throw new InvalidOperationException($"Multiple presence results for user ID '{userId}'");
            }

            return (response[0].Availability, response[0].Activity);
        }
    }
}
