namespace DaltroStore.Identity.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; } = string.Empty;

        public int ExpirationHours { get; set; }

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;
    }
}
