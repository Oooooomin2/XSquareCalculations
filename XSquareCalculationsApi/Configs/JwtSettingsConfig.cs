namespace XSquareCalculationsApi.Configs
{
    public sealed class JwtSettingsConfig
    {
        public const string SectionName = "JwtSettings";

        public string SecretKey { get; set; }
        public string SiteUrl { get; set; }
        public double ExpiredDay { get; set; }
    }
}
