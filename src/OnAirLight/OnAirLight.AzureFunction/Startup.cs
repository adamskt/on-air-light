using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnAirLight.AzureFunction.Graph;

[assembly: FunctionsStartup(typeof(OnAirLight.AzureFunction.Startup))]

namespace OnAirLight.AzureFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddOptions<GraphServiceConfig>()
                            .Configure<IConfiguration>((settings, configuration) =>
                            {
                                configuration.GetSection("GraphServiceConfig").Bind(settings);
                            });

             builder.Services.AddOptions<FunctionOptions>()
                            .Configure<IConfiguration>((settings, configuration) =>
                            {
                                configuration.GetSection("FunctionOptions").Bind(settings);
                            });
        }
    }
}
