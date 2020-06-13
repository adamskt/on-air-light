namespace OnAirLight.CommandLine.Config
{
    public class AppConfig
    {
        public string AppId { get; set; }
        public string TenantId { get; set; }
        public string Scopes { get; set; }
        public string UserId { get; set; }
        public string[] AllScopes => Scopes.Split(';');
    }
}
