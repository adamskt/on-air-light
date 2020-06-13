using OnAirLight.CommandLine.Authentication;
using OnAirLight.CommandLine.Config;
using OnAirLight.CommandLine.Graph;
using System;
using System.Threading;

namespace OnAirLight.CommandLine
{
    public class Program
    {

        private const int TimerSleepMilleseconds = 20 * 1000;

        private static readonly AppConfig _appConfig = ConfigService.LoadAppSettings();

        static void Main(string[] args)
        {
            WriteLine("Initializing from config...");

            var appConfig = ConfigService.LoadAppSettings();

            // Initialize the auth provider with values from appsettings.json
            var authProvider = new DeviceCodeAuthProvider(appConfig.AppId, appConfig.TenantId, appConfig.AllScopes);

            var graphService = new GraphService(authProvider);

            WriteLine($"Starting timer after {TimerSleepMilleseconds}ms.");

            using (var timer = new Timer(PollForPresence, graphService, TimerSleepMilleseconds, TimerSleepMilleseconds))
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }

        public static async void PollForPresence(object state)
        {
            WriteLine("Polling for presence info...");
            var service = (GraphService)state;
            var result = await service.GetPresenceAsync(_appConfig.UserId);

            WriteLine($"Polling complete = Availability: {result.Availability}, Activity: {result.Activity}");


        }

        private static void WriteLine(string message)
        {
            Console.WriteLine($"{DateTime.Now:O} - {message}");
        }
    }
}