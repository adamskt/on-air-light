using Microsoft.Extensions.Configuration;

namespace OnAirLight.CommandLine.Config
{
    public static class ConfigService
    {
        public static AppConfig LoadAppSettings()
        {
            var appConfig = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            // Check for required settings
            if (
                string.IsNullOrEmpty(appConfig["appId"])
                || string.IsNullOrEmpty(appConfig["scopes"])
                || string.IsNullOrEmpty(appConfig["tenantId"])
                || string.IsNullOrEmpty(appConfig["userId"])
                )
            {
                throw new InvalidConfigurationException();
            }

            var config = new AppConfig();

            appConfig.Bind(config);

            return config;
        }
    }
}
