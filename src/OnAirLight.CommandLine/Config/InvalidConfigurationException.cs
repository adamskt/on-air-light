using System;

namespace OnAirLight.CommandLine.Config
{
    public class InvalidConfigurationException : ApplicationException
    {
        public InvalidConfigurationException() : base("Missing or invalid appsettings.json")
        {

        }
    }
}
