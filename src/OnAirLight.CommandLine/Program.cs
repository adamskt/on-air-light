﻿using OnAirLight.CommandLine.Config;
using OnAirLight.CommandLine.Graph;
using OnAirLight.CommandLine.Hubitat;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnAirLight.CommandLine
{
    public class Program
    {

        private const int TimerSleepMilleseconds = 20 * 1000;

        private static AppConfig AppConfig { get; set; }
        private static GraphService GraphService { get; set; }
        private static HubitatService Hubitat { get; set; }
        private static Device TargetDevice { get; set; }

        static async Task Main(string[] args)
        {
            WriteLine("Initializing from config...");

            AppConfig = ConfigService.LoadAppSettings(args);

            GraphService = new GraphService(AppConfig);

            Hubitat = new HubitatService(AppConfig);

            var devices = await Hubitat.GetDevicesAsync();

            TargetDevice = devices.Single(x => string.Equals(x.Name, AppConfig.Hubitat.DeviceName, StringComparison.OrdinalIgnoreCase));

            await Hubitat.SendDeviceInitAsync(TargetDevice);

            WriteLine($"Starting timer after 100 ms.");

            using (var timer = new Timer(PollForPresence, Availability.PresenceUnknown, 100, TimerSleepMilleseconds))
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }

        public static async void PollForPresence(object state)
        {
            var lastAvailability = (Availability) state;
            WriteLine("Polling for presence info...");
            var result = await GraphService.GetPresenceAsync(AppConfig.Azure.UserId);

            WriteLine($"Polling complete = Availability: {result.availability}, Activity: {result.activity}");

            if (lastAvailability != result.availability)
            {
                await ChangeLightColor(result.availability);
            }

            state = result;


        }

        private static async Task ChangeLightColor(Availability availability)
        {
            switch (availability)
            {
                case Availability.Available:
                    await Hubitat.SendDevicePreset(TargetDevice, Preset.GreenFade);
                    break;
                case Availability.AvailableIdle:
                    await Hubitat.SendDevicePreset(TargetDevice, Preset.GreenFade);
                    break;
                case Availability.Away:
                    await Hubitat.SendDevicePreset(TargetDevice, Preset.YellowFade);
                    break;
                case Availability.BeRightBack:
                    break;
                case Availability.Busy:
                    await Hubitat.SendDevicePreset(TargetDevice, Preset.RedFade);
                    break;
                case Availability.BusyIdle:
                    await Hubitat.SendDevicePreset(TargetDevice, Preset.RedFade);
                    break;
                case Availability.DoNotDisturb:
                    await Hubitat.SendDevicePreset(TargetDevice, Preset.RedFade);
                    break;
                case Availability.Offline:
                    break;
                case Availability.PresenceUnknown:
                    break;
                default:
                    break;
            }
        }

        private static void WriteLine(string message)
        {
            Console.WriteLine($"{DateTime.Now:O} - {message}");
        }
    }
}