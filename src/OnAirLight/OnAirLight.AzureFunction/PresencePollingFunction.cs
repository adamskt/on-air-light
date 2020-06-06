using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnAirLight.AzureFunction.Graph;

namespace OnAirLight.AzureFunction
{
    public class PresencePollingFunction
    {
        public PresencePollingFunction(IOptions<GraphServiceConfig> graphOptions, IOptions<FunctionOptions> functionOptions)
        {
            GraphService = new GraphService(graphOptions.Value);
            FunctionOptions = functionOptions.Value;
        }

        private FunctionOptions FunctionOptions { get; }
        private IGraphService GraphService { get; }

        [FunctionName("PollForPresenceInfo")]
        public async Task PollForPresenceInfo([TimerTrigger("%timerSchedule%")]TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");


            log.LogInformation($"Getting presence info for: {FunctionOptions.GraphUserId}");

            var response = await GraphService.GetPresence(FunctionOptions.GraphUserId);

            log.LogInformation($"Got availability: '{response.Availability}' and activity: '{response.Activity}'");

            return;

        }
    }
}
