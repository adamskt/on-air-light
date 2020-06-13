using Microsoft.Graph;
using System;
using System.Threading.Tasks;

namespace OnAirLight.CommandLine.Graph
{
    public class GraphService
    {

        private readonly GraphServiceClient _graphClient;

        public GraphService(IAuthenticationProvider authProvider)
        {
            _graphClient = new GraphServiceClient(authProvider);
        }

         public async Task<(string Availability, string Activity)> GetPresenceAsync(string userId)
        {
            var response = await _graphClient.Communications.GetPresencesByUserId(new[] { userId })
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
