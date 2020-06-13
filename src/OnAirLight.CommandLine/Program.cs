using OnAirLight.CommandLine.Authentication;
using OnAirLight.CommandLine.Config;
using OnAirLight.CommandLine.Graph;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnAirLight.CommandLine
{
    public class Program
    {
        const int TwoMinutes = 120 * 1000;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Initializing from config...");

            var appConfig = ConfigService.LoadAppSettings();

            // Initialize the auth provider with values from appsettings.json
            var authProvider = new DeviceCodeAuthProvider(appConfig.AppId, appConfig.TenantId, appConfig.AllScopes);

            var graphService = new GraphService(authProvider);

            Console.WriteLine("Starting 2 minute timer");


            var timer = new Timer(async (state)=> 
            {
                var service = (GraphService) state;
                var result = await service.GetPresenceAsync(appConfig.UserId);

                Console.WriteLine($"Availability: {result.Availability}");
                Console.WriteLine($"Activity: {result.Activity}");
            }, graphService, 100, TwoMinutes);

            Console.ReadLine();
            timer.Dispose();


        }
    }
}