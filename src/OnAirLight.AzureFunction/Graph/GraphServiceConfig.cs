namespace OnAirLight.AzureFunction.Graph
{
    public class GraphServiceConfig
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public string TenantId { get; set; }

        public string Authority => $"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0";
    }
}
