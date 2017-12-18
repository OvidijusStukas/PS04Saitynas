namespace ECoinTracker.Configurations
{
    public class ApplicationConfiguration
    {
		public string JwtTokenSecretKey { get; set; }
		public string IssuerName { get; set; }
		public string AudienceName { get; set; }
    }
}
