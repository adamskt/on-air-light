using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace OnAirLight.AzureFunction
{
    public static class PresencePollingFunction
    {
        [FunctionName("PollForPresenceInfo")]
        public static void Run([TimerTrigger("%timerSchedule%")]TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
